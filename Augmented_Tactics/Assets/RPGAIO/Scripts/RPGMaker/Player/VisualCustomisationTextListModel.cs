using System.Collections.Generic;
using System.Linq;
using LogicSpawn.RPGMaker.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace LogicSpawn.RPGMaker
{
    public class VisualCustomisationTextListModel : VisualCustomiser
    {
        public Text TitleText;
        public Text SelectedOption;
        
        private int _curOption;
        //private SkinnedMeshRenderer targetRenderer;
        //private List<Material> targetMaterials;


        public void Init(VisualCustomisation customisation)
        {
            VisualCustomisation = customisation;
            TitleText.text = VisualCustomisation.Identifier;

            cached_targets = new List<GameObject>();

            if (VisualCustomisation.CustomisationType == VisualCustomisationType.GameObject)
            {
                //Cache values
                VisualCustomisation.TargetedGameObjectNames.ForEach(t => cached_targets.Add(FindTargetObject(t)));

                //Set Initial Value and initial colors
                _curOption = 0;
                SetOption();
            }
        }

        public void NextOption()
        {
            _curOption++;
            if (_curOption > VisualCustomisation.TargetedGameObjectNames.Count - 1)
            {
                _curOption = 0;
            }

            SetOption();
        }

        private void SetOption()
        {
            SelectedOption.text = VisualCustomisation.LabelOptions[_curOption];
            SetTextOption(VisualCustomisation.TargetedGameObjectNames[_curOption]);
        }

        public void PrevOption()
        {
            _curOption--;
            if (_curOption < 0)
            {
                _curOption = VisualCustomisation.TargetedGameObjectNames.Count - 1;
            }

            SetOption();
        }

    }
}