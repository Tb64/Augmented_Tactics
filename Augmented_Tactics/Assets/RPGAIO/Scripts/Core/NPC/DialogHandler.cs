using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Testing;
using LogicSpawn.RPGMaker.API;
using LogicSpawn.RPGMaker.Generic;
using UnityEngine;

namespace LogicSpawn.RPGMaker.Core
{
    public class DialogHandler : MonoBehaviour
    {
        public GUISkin mySkin;
        public static DialogHandler Instance;
        public InteractiveObjectMono DialogNpc;
        public NodeChain DialogNodeChain;
        public NodeChain OldNodeChain;

        public bool Interacting;

        public Node CurrentNode
        {
            get { return DialogNodeChain != null ? DialogNodeChain.CurrentNode : null; }
        }
        public DialogNode NpcResponse
        {
            get { return CurrentNode as DialogNode; }
        }

        public bool EvaluatingQuestDialog;
        public bool EvaluatingQuestCompleteDialog;
        public string EvaluatingQuestID = null;
        public string PostQuestEvaluationNodeID = null;

        public QuestCondition dialogQuestCondition = null;

        private CameraMode _oldCameraMode;
        private List<PlayerResponseNode> _responses;
        void Awake()
        {
            Instance = this;
            _responses = new List<PlayerResponseNode>();
        }

        //        void OnGUI()
        //        {
        //            GUI.skin = mySkin;
        //            if (DialogNodeChain != null)
        //            {
        //                GetObject.PlayerMono.Controller.Interacting = true;
        //
        //                if (CurrentNode is DialogNode)
        //                {
        //                    var npcResponseNode = CurrentNode as DialogNode;
        //                    GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 200, 500, 80), DialogNodeChain.Name, "DialogText");
        //                    GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height / 2 - 100, 500, 200), npcResponseNode.DialogText,"DialogText");
        //
        //                    GUI.Box(new Rect(Screen.width / 2 - 250, Screen.height - 250, 500, 240),"");
        //                    GUILayout.BeginArea(new Rect(Screen.width / 2 - 250, Screen.height - 250, 500, 240));
        //                    
        //                    foreach (var nextPoint in npcResponseNode.NextNodeLinks.Where(n => !string.IsNullOrEmpty(n.ID)))
        //                    {
        //                        var node = DialogNodeChain.GetNode(nextPoint.ID);
        //                        if(node is PlayerResponseNode)
        //                        {
        //                            var playerReponseNode = node as PlayerResponseNode;
        //                            if (GUILayout.Button("> " + playerReponseNode.DialogText, "PlayerResponse", GUILayout.Height(60)))
        //                            {
        //                                DialogNodeChain.Goto(node.ID);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            var subChain = new NodeChain(DialogNodeChain, node.ID);
        //                            while(!(subChain.CurrentNode is PlayerResponseNode) && subChain.CurrentNode != null)
        //                            {
        //                                subChain.Evaluate();
        //                            }
        //
        //                            if(subChain.CurrentNode is PlayerResponseNode)
        //                            {
        //                                var playerReponseNode = subChain.CurrentNode as PlayerResponseNode;
        //                                if (GUILayout.Button("> " + playerReponseNode.DialogText, GUILayout.Height(60)))
        //                                {
        //                                    DialogNodeChain.Goto(playerReponseNode.ID);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    GUILayout.EndArea();
        //                }
        //            }
        //        }

        private void CheckForUpdate()
        {
            CheckResponses();
            DialogUI.Instance.CheckForUpdate();
        }

        void Update()
        {
            Interacting = DialogNodeChain != null;

            if (DialogNodeChain != null)
            {
                if (DialogNodeChain.Done)
                {
                    EndDialog();
                }
                else
                {
                    if (!(DialogNodeChain.CurrentNode is DialogNode))
                    {
                        DialogNodeChain.Evaluate();
                        CheckForUpdate();
                    }
                }
            }
        }

        public void CheckResponses()
        {
            _responses = new List<PlayerResponseNode>();
            if (DialogNodeChain != null)
            {
                GetObject.PlayerMono.Controller.Interacting = true;

                if (CurrentNode is DialogNode)
                {
                    var npcResponseNode = CurrentNode as DialogNode;


                    foreach (var nextPoint in npcResponseNode.NextNodeLinks.Where(n => !string.IsNullOrEmpty(n.ID)))
                    {
                        var node = DialogNodeChain.GetNode(nextPoint.ID);
                        if (node is PlayerResponseNode)
                        {
                            var playerReponseNode = node as PlayerResponseNode;
                            _responses.Add(playerReponseNode);
                        }
                        else
                        {
                            var subChain = new NodeChain(DialogNodeChain, node.ID);
                            while (!(subChain.CurrentNode is PlayerResponseNode) && subChain.CurrentNode != null)
                            {
                                subChain.Evaluate();
                            }

                            if (subChain.CurrentNode is PlayerResponseNode)
                            {
                                var playerReponseNode = subChain.CurrentNode as PlayerResponseNode;
                                _responses.Add(playerReponseNode);
                            }
                        }
                    }
                }
            }
        }

        public List<PlayerResponseNode> GetResponses()
        {
            return _responses;
        }

        public void ChooseResponse(PlayerResponseNode response)
        {
            DialogNodeChain.Goto(response.ID);
            CheckForUpdate();
        }

        public void EndDialog()
        {
            var cam = GetObject.RPGCamera;
            cam.cameraMode = Rm_RPGHandler.Instance.DefaultSettings.DefaultCameraMode;
            if (cam.cameraMode != CameraMode.Manual)
            {
                cam.transform.position = cam.desiredPosition;
            }

            DialogNpc = null;

            if (EvaluatingQuestDialog)
            {
                //todo: is this the right place?!
                if (!string.IsNullOrEmpty(PostQuestEvaluationNodeID))
                {
                    DialogNodeChain = OldNodeChain;
                    if(DialogNodeChain != null)
                    {
                        DialogNodeChain.Goto(PostQuestEvaluationNodeID);
                    }
                }
                else
                {
                    DialogNodeChain = null;
                    GetObject.PlayerMono.Controller.Interacting = false;
                }

                OldNodeChain = null;
                EvaluatingQuestDialog = false;
            }
            else if (EvaluatingQuestCompleteDialog)
            {
                //todo: is this the right place?!
                QuestHandler.Instance.CompleteQuest(EvaluatingQuestID);
                if (!string.IsNullOrEmpty(PostQuestEvaluationNodeID))
                {
                    DialogNodeChain = OldNodeChain;
                    if(DialogNodeChain != null)
                        DialogNodeChain.Goto(PostQuestEvaluationNodeID);
                }
                else
                {
                    DialogNodeChain = null;
                    GetObject.PlayerMono.Controller.Interacting = false;
                }
                OldNodeChain = null;
                EvaluatingQuestCompleteDialog = false;
            }
            else
            {
                if (dialogQuestCondition != null)
                {
                    dialogQuestCondition.IsDone = true;
                    RPG.Events.OnQuestStatusUpdate();
                    var deliverCondition = dialogQuestCondition as DeliverCondition;
                    if (deliverCondition != null)
                    {
                        GetObject.PlayerCharacter.Inventory.RemoveItem(deliverCondition.ItemToDeliverID);
                    }
                    dialogQuestCondition = null;
                }

                DialogNodeChain = null;
                GetObject.PlayerMono.Controller.Interacting = false;
            }

            CheckForUpdate();
        }

        public void BeginQuestDialog(string nodeTreeId, InteractiveObjectMono dialogObject = null, bool isQuest = false, string questId = "")
        {
            var nodeTree = Rm_RPGHandler.Instance.Nodes.DialogNodeBank.NodeTrees.FirstOrDefault(n => n.ID == nodeTreeId);
            if (nodeTree != null)
            {
                if (isQuest)
                {
                    EvaluatingQuestDialog = true;
                    EvaluatingQuestID = questId;
                    PostQuestEvaluationNodeID = string.IsNullOrEmpty(CurrentNode.NextNodeLinks[0].ID) ? null : CurrentNode.NextNodeLinks[0].ID;
                }

                var nodeChain = new NodeChain(nodeTree, typeof(DialogStartNode));
                OldNodeChain = DialogNodeChain;
                DialogNodeChain = nodeChain;
                DialogNodeChain.Evaluate();
            }

            CheckForUpdate();
        }

        public void BeginQuestCompleteDialog(string nodeTreeId, InteractiveObjectMono dialogObject = null, bool isQuest = false, string questId = "")
        {
            var nodeTree = Rm_RPGHandler.Instance.Nodes.DialogNodeBank.NodeTrees.FirstOrDefault(n => n.ID == nodeTreeId);
            EvaluatingQuestCompleteDialog = true;
            EvaluatingQuestID = questId;

            if (nodeTree != null)
            {
                if (isQuest)
                {
                    EvaluatingQuestCompleteDialog = true;
                    EvaluatingQuestID = questId;
                    PostQuestEvaluationNodeID = string.IsNullOrEmpty(CurrentNode.NextNodeLinks[0].ID) ? null : CurrentNode.NextNodeLinks[0].ID;
                }

                var nodeChain = new NodeChain(nodeTree, typeof(DialogStartNode));
                OldNodeChain = DialogNodeChain;
                DialogNodeChain = nodeChain;
                DialogNodeChain.Evaluate();
            }

            CheckForUpdate();
        }

        public void BeginDialog(string nodeTreeId, InteractiveObjectMono dialogObject = null)
        {
            if (DialogNodeChain != null)
            {
                EndDialog();
            }
            DialogNpc = dialogObject ?? InteractiveObjectMono.CurrentInteraction;

            //Debug.Log(nodeTreeId);
            var nodeTree = Rm_RPGHandler.Instance.Nodes.DialogNodeBank.NodeTrees.FirstOrDefault(n => n.ID == nodeTreeId);
            if (nodeTree != null)
            {
                var nodeChain = new NodeChain(nodeTree, typeof(DialogStartNode));
                DialogNodeChain = nodeChain;
                DialogNodeChain.Evaluate();
            }
            else
            {
                EndDialog();
            }

            CheckForUpdate();
        }

        public void BeginDialog(QuestCondition questCondition, InteractiveObjectMono dialogObject = null)
        {
            dialogQuestCondition = questCondition;

            if (questCondition.ConditionType == ConditionType.Interact)
            {
                BeginDialog((questCondition as InteractCondition).InteractionNodeTreeID, dialogObject);
            }
            else if (questCondition.ConditionType == ConditionType.Deliver)
            {
                //todo: deliverInteraction
                BeginDialog((questCondition as DeliverCondition).InteractionNodeTreeID, dialogObject);
            }
        }
    }
}