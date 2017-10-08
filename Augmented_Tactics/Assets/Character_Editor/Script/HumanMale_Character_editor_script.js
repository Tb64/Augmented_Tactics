#pragma strict
import System.IO;
				
// Here is what you need to change in the inspector when changing the character in the scene or if you lose the prefab connection

var Character : GameObject; 					//this is the prefab you will export you can plug the thirdperson controler or the character itself			
var ModelRenderers : SkinnedMeshRenderer[];    	//Renderers for the LOD character model 
var cloak : GameObject[];	           			//Renderers for the LOD cloak model 
var ShortRobe : GameObject[];	       			//Renderers for the LOD ShortRobe model 	
var LongRobe : GameObject[];	    		   	//Renderers for the LOD LongRobe model 		
																							
		//  _____ _____  _______ _   _ ___ ___ 
		// |_   _| __\ \/ /_   _| | | | _ \ __|
		//   | | | _| >  <  | | | |_| |   / _| 
		//   |_| |___/_/\_\ |_|  \___/|_|_\___|


private var CharacterRace : String; 
								
//system
var atlasBase:Texture2D;  //the full texture atlas template // the base texture

var planeRend : Renderer;    // the material rendere fore the model
var CombineAll : Texture2D;		// the texture that combine all other

var saveButton : Collider; // save button 

private var atlasBaseSearchSkinFace : String[];//search for the texture at directory 
private var atlasBaseSkinFace : Texture2D[];   //as Texture2D[]; array of headAdd texture
private var skinFaceQ : int;                   //texture quantity array.length
private var skinFaceN : int;                   //Texture number to select
private var skinFaceS : Texture2D;             //The selected texture2D in the array
 var faceSkinButtonNext : Collider; //Button
 var faceSkinButtonBack : Collider; //Button
 var faceSkinButtonClear: Collider; //Button
private var colorNumber : int;
 var skinColorButtonNext : Collider;    //Button
 var skinColorButtonBack : Collider;    //Button
 var skinColorButtonClear : Collider;
private var ColorSkinCombine : Texture2D;

private var HeadAddCombine : Texture2D;
private var headAddSearch : String[];          //search for the texture at directory 
private var headAdd : Texture2D[];             //as Texture2D[]; array of headAdd texture
private var headAddQ : int;                    //texture quantity array.length
private var headAddN : int;                    //Texture number to select
 var headAddButtonBack : Collider;      //Button
 var headAddButtonNext : Collider;      //Button
 var headAddButtonClear : Collider;      //Button

private var EyeBrowCombine : Texture2D;
private var EyeBrowSearch : String[];          //search for the texture at directory 
private var EyeBrow : Texture2D[];             //as Texture2D[]; array of EyeBrow texture
private var EyeBrowQ : int;                    //texture quantity array.length
private var EyeBrowN : int;                    //Texture number to select
 var EyeBrowButtonBack : Collider;      //Button
 var EyeBrowButtonNext : Collider;      //Button
 var EyeBrowButtonClear : Collider;      //Button

private var ScarsCombine : Texture2D;
private var ScarsSearch : String[];          //search for the texture at directory 
private var Scars : Texture2D[];             //as Texture2D[]; array of Scars texture
private var ScarsQ : int;                    //texture quantity array.length
private var ScarsN : int;                    //Texture number to select
 var ScarsButtonBack : Collider;      //Button
 var ScarsButtonNext : Collider;      //Button
 var ScarsButtonClear : Collider;      //Button

private var BeardCombine : Texture2D;
private var BeardSearch : String[];          //search for the texture at directory 
private var Beard : Texture2D[];             //as Texture2D[]; array of Beard texture
private var BeardQ : int;                    //texture quantity array.length
private var BeardN : int;                    //Texture number to select
 var BeardButtonBack : Collider;      //Button
 var BeardButtonNext : Collider;      //Button
 var BeardButtonClear : Collider;      //Button

private var HairSkullCombine : Texture2D;
private var HairSkullSearch : String[];          //search for the texture at directory 
private var HairSkull : Texture2D[];             //as Texture2D[]; array of HairSkull texture
private var HairSkullQ : int;                    //texture quantity array.length
private var HairSkullN : int;                    //Texture number to select
 var HairSkullButtonBack : Collider;      //Button
 var HairSkullButtonNext : Collider;      //Button
 var HairSkullButtonClear : Collider;
 
private var EyeSearch : String[];          //search for the texture at directory 
private var Eye : Texture2D[];             //as Texture2D[]; array of Eye texture
private var EyeQ : int;                    //texture quantity array.length
private var EyeN : int;                    //Texture number to select
 var EyeButtonBack : Collider;      //Button
 var EyeButtonNext : Collider;      //Button
 var EyeButtonClear : Collider;      //Button

private var EyeCombine : Texture2D;
private var PantSearch : String[];          //search for the texture at directory 
private var Pant : Texture2D[];             //as Texture2D[]; array of Pant texture
private var PantQ : int;                    //texture quantity array.length
private var PantN : int;                    //Texture number to select
 var PantButtonBack : Collider;      //Button
 var PantButtonNext : Collider;      //Button<
 var PantButtonClear : Collider;      //Button
private var PantCombine : Texture2D;


private var TorsoSearch : String[];          //search for the texture at directory 
private var Torso : Texture2D[];             //as Texture2D[]; array of Torso texture
private var TorsoQ : int;                    //texture quantity array.length
private var TorsoN : int;                    //Texture number to select
 var TorsoButtonBack : Collider;      //Button
 var TorsoButtonNext : Collider;      //Button
 var TorsoButtonClear : Collider;      //Button
private var TorsoCombine : Texture2D;

private var ShoeSearch : String[];          //search for the texture at directory 
private var Shoe : Texture2D[];             //as Texture2D[]; array of Shoe texture
private var ShoeQ : int;                    //texture quantity array.length
private var ShoeN : int;                    //Texture number to select
 var ShoeButtonBack : Collider;      //Button
 var ShoeButtonNext : Collider;      //Button
 var ShoeButtonClear : Collider;      //Button
private var ShoeCombine : Texture2D;


private var GloveSearch : String[];          //search for the texture at directory 
private var Glove : Texture2D[];             //as Texture2D[]; array of Glove texture
private var GloveQ : int;                    //texture quantity array.length
private var GloveN : int;                    //Texture number to select
 var GloveButtonBack : Collider;      //Button
 var GloveButtonNext : Collider;      //Button
 var GloveButtonClear : Collider;      //Button
private var GloveCombine : Texture2D;

private var BeltSearch : String[];          //search for the texture at directory 
private var Belt : Texture2D[];             //as Texture2D[]; array of Belt texture
private var BeltQ : int;                    //texture quantity array.length
private var BeltN : int;                    //Texture number to select
 var BeltButtonBack : Collider;      //Button
 var BeltButtonNext : Collider;      //Button
 var BeltButtonClear : Collider;      //Button
private var BeltCombine : Texture2D;

var PilosityColorButtonNext : Collider;
var PilosityColorButtonBack : Collider;
var PilosityColorButtonClear : Collider;
private var colorPilosityNumber = 0;
 var RandomFaceButton:Collider;
 var randTunique = 0;		//number of tunique randomized
 var RandomTuniqueButton : Collider;	//button collider form random tunique
 var cloak_next :Collider;
 var cloak_back :Collider;
 var cloak_clear :Collider;
 var cloakSearch : String[];          //search for the texture at directory 
 var cloakTex : Texture2D[];             //as Texture2D[]; array of Belt texture
 var cloakQ : int;                    //texture quantity array.length
 var cloakN : int;                    //Texture number to select
 
 var robeLongCombine : Texture2D;
 var robeLongB = false;
 var robeShortSearch : String[];          //search for the texture at directory 
 var robeShortTex : Texture2D[];             //as Texture2D[]; array of Belt texture
 var robeShortQ : int;                    //texture quantity array.length
 var robeShortN : int;                    //Texture number to select
 var robeShort_next :Collider;
 var robeShort_back :Collider;
 var robeShort_clear :Collider;
 
 var robeShortCombine : Texture2D;
 var robeShortB = false;
 var robeLongSearch : String[];          //search for the texture at directory 
 var robeLongTex : Texture2D[];             //as Texture2D[]; array of Belt texture
 var robeLongQ : int;                    //texture quantity array.length
 var robeLongN : int;                    //Texture number to select
 var robeLong_next :Collider;
 var robeLong_back :Collider;
 var robeLong_clear :Collider;
 
		//    _   ___ __  __  ___  ___ 
		//   /_\ | _ \  \/  |/ _ \| _ \
		//  / _ \|   / |\/| | (_) |   /
		// /_/ \_\_|_\_|  |_|\___/|_|_\



//feedback armor texture output
var NoneMat : Material;
var planeArmor2048 : GameObject;
var planeArmor1024 : GameObject;
var planeArmor512 : GameObject;
var planeArmor2048Tex : Transform[];
var planeArmor1024Tex : Transform[];
var planeArmor512Tex : Transform;
var MatArmor : Material;
var MatArmorPart : Material[];
var MatItemNumber = 0;

// pilosity texture2D array for changing color on "ArmorPart pilosity" such as hair and beard
var hairTex : Texture2D[];
var hairTexQ =0;
var lodHairGet : Renderer[];
var jawTex : Texture2D[];
var jawTexQ =0;
var lodJawGet : Renderer[];

// Moving Uw
var itemNumber = 0;
var armorsParts = new MeshFilter[3];
var equipedArmor : GameObject[];
var AllArmor : GameObject[];

var TextureArmor : Texture2D;       //The atlas for FindObjectsOfTypeAll armor part
var AllArmorsPartN : int;          //the numero of the armor part 
var AllArmorsPartQ : int;         //quantity of armor part equiped
var ArmorpartEquip : boolean[];		// thos boolean say if a item by categorie is equiped or not 
var AllArmorsPart : GameObject[];  // array of equiped stuff  
var AllArmorsPartMesh : MeshFilter[];
var TextureArmorPart : Texture2D[];
var weaponRArmorSTexture : Texture2D;


var AllArmorPartButton : Collider;

//base anchor for item by category
var anchorHair : Transform;
var anchorHead : Transform;
var anchorJaw : Transform;
var anchorEye : Transform;
var anchorTorso : Transform;
var anchorTorsoAdd : Transform;
var anchorBelt : Transform;
var anchorBeltAdd : Transform;
var anchorShoulderR : Transform;
var anchorShoulderL : Transform;
var anchorArmR : Transform;
var anchorArmL : Transform;
var anchorLegR : Transform;
var anchorLegL : Transform;
var anchorWeaponL : Transform;
var anchorWeaponR : Transform;
var anchorFX : Transform;
// Quantity of item by category
private var headQ = 0;
private var hairModelQ = 0;
private var jawQ = 0;
private var eyeQ = 0;
private var torsoQ = 0;
private var torsoAddQ = 0;
private var beltQ = 0;
private var beltAddQ = 0;
private var shoulderRQ = 0;
private var shoulderLQ = 0;
private var armRQ = 0;
private var armLQ = 0;
private var legRQ = 0;
private var legLQ = 0;
private var weaponRQ = 0;
private var weaponLQ = 0;
private var FXQ = 0;
// Item number selection by category
 var headN = 0;
 var hairModelN = 0;
private var jawN = 0;
private var eyeN = 0;
private var torsoN = 0;
private var torsoAddN = 0;
private var beltN = 0;
private var beltAddN = 0;
private var shoulderRN = 0;
private var shoulderLN = 0;
private var armRN = 0;
private var armLN = 0;
private var legRN = 0;
private var legLN = 0;
private var weaponRN = 0;
private var weaponLN = 0;
private var FXN = 0;
// Item selected by category
private var headArmorS : GameObject;
private var hairModelS : GameObject;
private var jawS : GameObject;
private var eyeS : GameObject;
private var torsoArmorS : GameObject;
private var torsoAddArmorS : GameObject;
private var beltArmorS : GameObject;
private var beltAddArmorS : GameObject;
private var shoulderRArmorS : GameObject;
private var shoulderLArmorS : GameObject;
private var armRArmorS : GameObject;
private var armLArmorS : GameObject;
private var legRArmorS : GameObject;
private var legLArmorS : GameObject;
private var weaponRArmorS : GameObject;
private var weaponLArmorS : GameObject;
private var FXArmorS : GameObject;
//ARRAY of Armor part, Weapon and Fx 
private var headArmorSearch : String[];
private var headArmor : GameObject[];
private var eyeAddArmorSearch : String[];
private var eyeAddArmor : GameObject[];
private var hairModel : GameObject[];
private var jaw : GameObject[];
private var torsoArmor :GameObject[];
private var torsoArmorSearch : String[];
private var torsoAddArmor : GameObject[];
private var torsoAddArmorSearch : String[];
private var beltArmor :GameObject[];
private var beltArmorSearch : String[];
private var beltAddArmor :GameObject[];
private var shoulderRArmorSearch : String[];
private var shoulderRArmor :GameObject[];
private var shoulderLArmorSearch : String[];
private var shoulderLArmor :GameObject[];
private var armRArmorSearch : String[];
private var armRArmor :GameObject[];
private var armLArmorSearch : String[];
private var armLArmor :GameObject[];
private var legRArmorSearch : String[];
private var legRArmor :GameObject[];
private var legLArmorSearch : String[];
private var legLArmor : GameObject[];
private var weaponRArmor :GameObject[];
private var weaponLArmor :GameObject[];
private var FXArmor :GameObject[];
private var FXHead :GameObject[];
//ANCHOR BIPED BONES for armor and weapon 
var headAnchor : Transform;
var jawAnchor : Transform;
var torsoAnchor : Transform;
var beltAnchor : Transform;
var shoulderRAnchor : Transform;
var shoulderLAnchor : Transform;
var armRAnchor : Transform;
var armLAnchor : Transform;
var legRAnchor : Transform;
var legLAnchor : Transform;
var weaponRAnchor : Transform;
var weaponLAnchor : Transform;
var FXAnchor : Transform;
// Button next and back for armor/weapon/Fx 
var head_next : Collider;
var head_back : Collider;
var head_clear : Collider;
var hairModel_next : Collider;
var hairModel_back : Collider;
var hairModel_clear: Collider;
var eye_next : Collider;
var eye_back : Collider;
var eye_clear : Collider;
var jaw_next : Collider;
var jaw_back : Collider;
var jaw_clear : Collider;
var torso_next : Collider;
var torso_back : Collider;
var torso_clear : Collider;
var torsoAdd_next : Collider;
var torsoAdd_back : Collider;
var torsoAdd_clear : Collider;
var belt_next : Collider;
var belt_back : Collider;
var belt_clear : Collider;
var beltAdd_next : Collider;
var beltAdd_back : Collider;
var beltAdd_clear : Collider;
var shoulderR_next : Collider;
var shoulderR_back : Collider;
var shoulderR_clear : Collider;
var shoulderL_next : Collider;
var shoulderL_back : Collider;
var shoulderL_clear : Collider;
var armR_next : Collider;
var armR_back : Collider;
var armR_clear : Collider;
var armL_next : Collider;
var armL_back : Collider;
var armL_clear : Collider;
var legR_next : Collider;
var legR_back : Collider;
var legR_clear : Collider;
var legL_next : Collider;
var legL_back : Collider;
var legL_clear : Collider;
var weaponR_next : Collider;
var weaponR_back : Collider;
var weaponR_clear : Collider;
var weaponL_next : Collider;
var weaponL_back : Collider;
var weaponL_clear : Collider;
var FX_next : Collider;
var FX_back : Collider;
var FX_clear : Collider;
var randomButtonTex2 : Collider;
var randomButtonTex1 : Collider;
var randomButton : Collider;
var savePrefabButton : Collider;
var clearAllButton : Collider;
var clearTex2 : Collider;
var clearTex1 : Collider;

// this variable increase for each created prefab during the play mode (runtime)
var SavedPrefabNum = 0;


////////////////////////////////////////////////////////   ______________    ____  ______////////////////////////////////////////////////////////
////////////////////////////////////////////////////////  / ___/_  __/   |  / __ \/_  __/////////////////////////////////////////////////////////
////////////////////////////////////////////////////////  \__ \ / / / /| | / /_/ / / /   ////////////////////////////////////////////////////////
//////////////////////////////////////////////////////// ___/ // / / ___ |/ _, _/ / /    ////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////____//_/ /_/  |_/_/ |_| /_/     ////////////////////////////////////////////////////////
////////////////////////////////////////////////////////                    			 ////////////////////////////////////////////////////////


// Function start 
function Start () {
		
		// this will allow in the future update more character race
		CharacterRace = "HumanMale";

		//  _____ _____  _______ _   _ ___ ___ 
		// |_   _| __\ \/ /_   _| | | | _ \ __|
		//   | | | _| >  <  | | | |_| |   / _| 
		//   |_| |___/_/\_\ |_|  \___/|_|_\___|

	CombineAll = atlasBase;
	skinFaceS = atlasBase;
																		////LOADING CHARACTER TEXTURE////
                                                   //At start this part of the script put textures of the character in aray liste of texture 2d  
                                                   // the script search in project folder
                                                  //       /!\ need to have a proper nomenclature folder and texture 


// AtlasBase Texture SkinFace
	atlasBaseSearchSkinFace = AssetDatabase.FindAssets ("HumanMale_Color"+colorNumber+"_FaceSkin t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Color/Color"+colorNumber+""]); //Search For Color0 Texture get all SkinFace
	atlasBaseSkinFace = new Texture2D[atlasBaseSearchSkinFace.length];
	for(var a = 0; a < atlasBaseSearchSkinFace.Length; ++a){
		atlasBaseSkinFace[a] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Color/Color0/HumanMale_Color0_FaceSkin" +a+ ".png", Texture2D) as Texture2D;}
	skinFaceQ = atlasBaseSkinFace.Length;
	                                             
// HeadAdd Texture in folder 
	headAddSearch = AssetDatabase.FindAssets ("headAdd t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Head_add"]);								 //search texture by name for array length
	headAdd = new Texture2D[headAddSearch.length];                                                              								//resize array
	for(var b = 0; b < headAddSearch.Length; ++b){																      						   //fill with Texture2D
		headAdd[b] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Head_add/headAdd" +b+ ".png", Texture2D) as Texture2D;}
    headAddQ = headAdd.Length;  

// EyeBrow Texture in folder
	EyeBrowSearch = AssetDatabase.FindAssets ("EyeBrow t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Eyebrow/Color0"]);								 //search texture by name for array length
	EyeBrow = new Texture2D[EyeBrowSearch.length];                                                              								//resize array
	for(var c = 0; c < EyeBrowSearch.Length; ++c){																      						   //fill with Texture2D
		EyeBrow[c] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Eyebrow/Color0/EyeBrow" +c+ "_Color0.png", Texture2D) as Texture2D;}
    EyeBrowQ = EyeBrow.Length;  
                                                                                                                  //texture Quantity  
 // Scars Texture in folder  
	ScarsSearch = AssetDatabase.FindAssets ("Scars t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Scars"]);								 //search texture by name for array length
	Scars = new Texture2D[ScarsSearch.length];                                                              								//resize array
	for(var d = 0; d < ScarsSearch.Length; ++d){																      						   //fill with Texture2D
		Scars[d] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Scars/Scars" +d+ ".png", Texture2D) as Texture2D;}
    ScarsQ = Scars.Length;  
  
  // Beard Texture in folder  
	BeardSearch = AssetDatabase.FindAssets ("Beard t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Beard/Color0"]);								 //search texture by name for array length
	Beard = new Texture2D[BeardSearch.length];                                                              								//resize array
	for(var e = 0; e < BeardSearch.Length; ++e){																      						   //fill with Texture2D
		Beard[e] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Beard/Color0/Beard" +e+ "_Color0.png", Texture2D) as Texture2D;}
    BeardQ = Beard.Length;  
    
 // HairSkull Texture in folder  
	HairSkullSearch = AssetDatabase.FindAssets ("HairSkull t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_HairSkull/Color0"]);								 //search texture by name for array length
	HairSkull = new Texture2D[HairSkullSearch.length];                                                              								//resize array
	for(var g = 0; g < HairSkullSearch.Length; ++g){																      						   //fill with Texture2D
		HairSkull[g] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_HairSkull/Color0/HumanMale_HairSkull" +g+ "_Color0.png", Texture2D) as Texture2D;}
    HairSkullQ = HairSkull.Length;  

// Eye Texture in folder 
	EyeSearch = AssetDatabase.FindAssets ("Eye t:texture2D", ["Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Eye"]);								 //search texture by name for array length
	Eye = new Texture2D[EyeSearch.length];                                                              								//resize array
	for(var f = 0; f < EyeSearch.Length; ++f){																      						   //fill with Texture2D
		Eye[f] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Eye/Eye" +f+ ".png", Texture2D) as Texture2D;}
    EyeQ = Eye.Length; 

// Pant Texture in folder 
	PantSearch = AssetDatabase.FindAssets ("Pant t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Pant"]);								 //search texture by name for array length
	Pant = new Texture2D[PantSearch.length];                                                              								//resize array
	for(var i = 0; i < PantSearch.Length; ++i){																      						   //fill with Texture2D
		Pant[i] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Pant/Pant" +i+ ".png", Texture2D) as Texture2D;}
    PantQ = Pant.Length;

// Torso Texture in folder 
	TorsoSearch = AssetDatabase.FindAssets ("Torso t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Torso"]);								 //search texture by name for array length
	Torso = new Texture2D[TorsoSearch.length];                                                              								//resize array
	for(var j = 0; j < TorsoSearch.Length; ++j){																      						   //fill with Texture2D
		Torso[j] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Torso/Torso" +j+ ".png", Texture2D) as Texture2D;}
    TorsoQ = Torso.Length; 

// Shoe Texture in folder 
	ShoeSearch = AssetDatabase.FindAssets ("Shoe t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Shoe"]);								 //search texture by name for array length
	Shoe = new Texture2D[ShoeSearch.length];                                                              								//resize array
	for(var k = 0; k < ShoeSearch.Length; ++k){																      						   //fill with Texture2D
		Shoe[k] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Shoe/Shoe" +k+ ".png", Texture2D) as Texture2D;}
    ShoeQ = Shoe.Length;                                                                                                                                     //texture Quantity 

// Glove Texture in folder 
	GloveSearch = AssetDatabase.FindAssets ("Glove t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Glove"]);								 //search texture by name for array length
	Glove = new Texture2D[GloveSearch.length];                                                              								//resize array
	for(var l = 0; l < GloveSearch.Length; ++l){																      						   //fill with Texture2D
		Glove[l] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Glove/Glove" +l+ ".png", Texture2D) as Texture2D;}
    GloveQ = Glove.Length;   

// Belt Texture in folder 
	BeltSearch = AssetDatabase.FindAssets ("Belt t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/Belt"]);								 //search texture by name for array length
	Belt = new Texture2D[BeltSearch.length];                                                              								//resize array
	for(var m = 0; m < BeltSearch.Length; ++m){																      						   //fill with Texture2D
		Belt[m] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/Belt/Belt" +m+ ".png", Texture2D) as Texture2D;}
    BeltQ = Belt.Length;   
// Cloak Texture in folder 

   	cloakSearch = AssetDatabase.FindAssets ("A_Cloak_ t:texture2D", ["Assets/Character_Editor/Textures/Armor/Cloak"]);								 //search texture by name for array length
	cloakTex = new Texture2D[cloakSearch.length];                                                              								//resize array
	for(var cl = 0; cl < cloakSearch.Length; ++cl){																      						   //fill with Texture2D
		cloakTex[cl] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Armor/Cloak/A_Cloak_" +cl+ ".png", Texture2D) as Texture2D;}
    cloakQ = cloakTex.Length;
	for(var clo2 = 0;clo2 < cloak.Length; clo2++ ){
	cloak[clo2].SetActive(false);
	}
// robeLong Texture in folder 
	robeLongSearch = AssetDatabase.FindAssets ("RobeLong t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/RobeLong"]);								 //search texture by name for array length
	robeLongTex = new Texture2D[robeLongSearch.length];                                                              								//resize array
	for(var rl = 0; rl < robeLongSearch.Length; ++rl){																      						   //fill with Texture2D
		robeLongTex[rl] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/RobeLong/RobeLong" +rl+ ".png", Texture2D) as Texture2D;}
    robeLongQ = robeLongTex.Length;

// robeShort Texture in folder 
	robeShortSearch = AssetDatabase.FindAssets ("RobeShort t:texture2D", ["Assets/Character_Editor/Textures/CharacterOutfit/RobeShort"]);								 //search texture by name for array length
	robeShortTex = new Texture2D[robeShortSearch.length];                                                              								//resize array
	for(var rsh = 0; rsh < robeShortSearch.Length; ++rsh){																      						   //fill with Texture2D
		robeShortTex[rsh] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/CharacterOutfit/RobeShort/RobeShort" +rsh+ ".png", Texture2D) as Texture2D;}
    robeShortQ = robeShortTex.Length;    
   

 // lunch the texturing loop in the start function 
 //the character model receive the default texture combined 
 // default are the 0 texture in here folder(most of them are empty transparent png, only the faceskin colorskin and the pant have pixels)                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       //texture Quantity 
ColorSkinF();
SkinFaceCombineF();

		//    _   ___ __  __  ___  ___ 
		//   /_\ | _ \  \/  |/ _ \| _ \
		//  / _ \|   / |\/| | (_) |   /
		// /_/ \_\_|_\_|  |_|\___/|_|_\


	//get biped part 
	//!\\ YOU SHOULD HAVE ONLY 1 BIPED WITH THE SAME NOMENCLATURE IN YOUR SCENE TO WORK PROPRELY
	
	headAnchor = GameObject.Find('Bip01_Head').transform; 
	jawAnchor = GameObject.Find('Bip01_Jaw').transform;	  
	torsoAnchor = GameObject.Find('Bip01_Spine3').transform;
	beltAnchor = GameObject.Find('Bip01_Pelvis').transform;
	shoulderRAnchor = GameObject.Find('Bip01_R_UpperArm').transform;
	shoulderLAnchor = GameObject.Find('Bip01_L_UpperArm').transform;
	armRAnchor = GameObject.Find('Bip01_R_Forearm').transform;
	armLAnchor = GameObject.Find('Bip01_L_Forearm').transform;
	legRAnchor = GameObject.Find('Bip01_R_Calf').transform;
	legLAnchor = GameObject.Find('Bip01_L_Calf').transform;
	weaponRAnchor = GameObject.Find('Bip01_R_Weapon').transform;
	weaponLAnchor = GameObject.Find('Bip01_L_Weapon').transform;
	FXAnchor = GameObject.Find('Bip01_Spine3').transform;

	
	// Set the number of the selected item for each categorie
	headN = 0;
	hairModelN =0;
	jawN = 0;
	eyeN = 0;
	torsoN = 0;
	torsoAddN = 0;
	beltN = 0;
	beltAddN = 0;
	shoulderRN = 0;
	shoulderLN = 0;
	armRN = 0;
	armLN = 0;
	legRN = 0;
	legLN = 0;
	FXN = 0;
	weaponRN = 0;
	weaponLN = 0;		
		
	// Find All Armor Part GameObject
	
	AllArmor = FindObjectsOfType(GameObject) as GameObject[];	
		
  		
    // Find Helm and store
    for (var obj=0; obj < AllArmor.length; obj++){if(AllArmor[obj].name.Contains("A_Helm_" + CharacterRace)){headQ ++;}}
	headArmor = new GameObject[headQ];          
    for(var p = 0; p < headQ; ++p){
     	 headArmor[p] = GameObject.Find("A_Helm_"+ CharacterRace+"_"+p+"");}
    
    // Find HairModel and store
    for (var hm=0; hm < AllArmor.length; hm++){if(AllArmor[hm].name.Contains("A_HairModel_" + CharacterRace)){hairModelQ ++;}}
	hairModel = new GameObject[hairModelQ];          
    for(var hmo = 0; hmo < hairModelQ; ++hmo){
     	 hairModel[hmo] = GameObject.Find("A_HairModel_" + CharacterRace+"_"+hmo+"");} 	 
	
	// Find JawModel and store
    for (var jw=0; jw < AllArmor.length; jw++){if(AllArmor[jw].name.Contains("A_Jaw_" + CharacterRace)){jawQ ++;}}
	jaw = new GameObject[jawQ];          
    for(var jwx = 0; jwx < jawQ; ++jwx){
     	 jaw[jwx] = GameObject.Find("A_Jaw_"+ CharacterRace+"_"+jwx+"");} 	 
	 
	  // Find TorsoArmor and store
	for (var to=0; to < AllArmor.length; to++){if(AllArmor[to].name.Contains("A_TorsoArmor_"+ CharacterRace)){torsoQ ++;}}
	torsoArmor = new GameObject[torsoQ];          
	for(var q = 0; q < torsoQ; ++q){
		torsoArmor[q] = GameObject.Find("A_TorsoArmor_"+ CharacterRace+"_"+q+"");}
	
	for (var be = 0; be < AllArmor.length; be++){if(AllArmor[be].name.Contains("A_Belt_"+ CharacterRace)){beltQ ++;}}
    beltArmor = new GameObject[beltQ]; 		
	for(var r = 0; r < beltQ; ++r){
	  	beltArmor[r] = GameObject.Find("A_Belt_"+ CharacterRace+"_"+r+"");}
	//			for ( var Helm in AllArmor){
	//	Debug.Log('trouvées');
	for (var toa = 0; toa < AllArmor.length; toa++){if(AllArmor[toa].name.Contains("A_TorsoAdd_"+ CharacterRace)){torsoAddQ ++;}}
    torsoAddArmor = new GameObject[torsoAddQ]; 	                                                              		
	for(var s = 0; s < torsoAddQ; ++s){
		 torsoAddArmor[s] = GameObject.Find("A_TorsoAdd_"+ CharacterRace+"_"+s+"");}

	for (var eyx = 0; eyx < AllArmor.length; eyx++){if(AllArmor[eyx].name.Contains("A_EyeAdd_"+ CharacterRace)){eyeQ ++;}}
    eyeAddArmor = new GameObject[eyeQ]; 	                                                              			
	for(var x = 0; x < eyeQ; ++x){																      						   //fill with Texture2D
		eyeAddArmor[x] = GameObject.Find("A_EyeAdd_"+ CharacterRace +"_"+x+"");}
	
	for (var shr = 0; shr < AllArmor.length; shr++){if(AllArmor[shr].name.Contains("A_Shoulder_R_"+ CharacterRace)){shoulderRQ ++;}}
    shoulderRArmor = new GameObject[shoulderRQ]; 	                        	
	for(var o = 0; o < shoulderRQ; ++o){																      						   //fill with Texture2D
	  shoulderRArmor[o] = GameObject.Find("A_Shoulder_R_"+ CharacterRace +"_"+o+"");}
			
	for (var shl = 0; shl < AllArmor.length; shl++){if(AllArmor[shl].name.Contains("A_Shoulder_L_"+ CharacterRace)){shoulderLQ ++;}}
    shoulderLArmor = new GameObject[shoulderLQ]; 	                        	
	for(var n = 0; n < shoulderLQ; ++n){																      						   //fill with Texture2D
	  shoulderLArmor[n] = GameObject.Find("A_Shoulder_L_"+ CharacterRace +"_"+n+"");}			

	for (var arr = 0; arr < AllArmor.length; arr++){if(AllArmor[arr].name.Contains("A_Arm_R_"+ CharacterRace)){armRQ ++;}}
    armRArmor = new GameObject[armRQ]; 	                        	
	for(var u = 0; u < armRQ; ++u){																      						   //fill with Texture2D
	  armRArmor[u] = GameObject.Find("A_Arm_R_"+ CharacterRace +"_"+u+"");}
	
	for (var arl = 0; arl < AllArmor.length; arl++){if(AllArmor[arl].name.Contains("A_Arm_L_"+ CharacterRace)){armLQ ++;}}
    armLArmor = new GameObject[armLQ]; 	                        	
	for(var t = 0; t < armLQ; ++t){																      						   //fill with Texture2D
	  armLArmor[t] = GameObject.Find("A_Arm_L_"+ CharacterRace +"_"+t+"");}
	
	for (var lel = 0; lel < AllArmor.length; lel++){if(AllArmor[lel].name.Contains("A_Leg_L_"+ CharacterRace)){legLQ ++;}}
    legLArmor = new GameObject[legLQ]; 	                        	
	for(var v = 0; v < legLQ; ++v){																      						   //fill with Texture2D
	  legLArmor[v] = GameObject.Find("A_Leg_L_"+ CharacterRace +"_"+v+"");}
	  
	for (var ler = 0; ler < AllArmor.length; ler++){if(AllArmor[ler].name.Contains("A_Leg_R_"+ CharacterRace)){legRQ ++;}}
    legRArmor = new GameObject[legRQ]; 	                        	
	for(var w = 0; w < legRQ; ++w){																      						   //fill with Texture2D
	  legRArmor[w] = GameObject.Find("A_Leg_R_"+ CharacterRace +"_"+w+"");}
		
	for (var wr = 0; wr < AllArmor.length; wr++){if(AllArmor[wr].name.Contains("W_R_")){weaponRQ ++;}}
    weaponRArmor = new GameObject[weaponRQ]; 	                        	
	for(var wir = 0; wir < weaponRQ; ++wir){																      						   //fill with Texture2D
	  weaponRArmor[wir] = GameObject.Find("W_R_"+wir+"");}
	  
	for (var wl = 0; wl < AllArmor.length; wl++){if(AllArmor[wl].name.Contains("W_L_")){weaponLQ ++;}}
    weaponLArmor = new GameObject[weaponLQ]; 	                        	
	for(var wil = 0; wil < weaponLQ; ++wil){																      						   //fill with Texture2D
	  weaponLArmor[wil] = GameObject.Find("W_L_"+wil+"");}
		
	for (var bea = 0; bea < AllArmor.length; bea++){if(AllArmor[bea].name.Contains("A_BeltAdd_"+ CharacterRace)){beltAddQ ++;}}
    beltAddArmor = new GameObject[beltAddQ]; 		
	for(var ra = 0; ra < beltAddQ; ++ra){
	  	beltAddArmor[ra] = GameObject.Find("A_BeltAdd_"+ CharacterRace+"_"+ra+"");}

	 // Find FxTorso and store
	for (var fxt=0; fxt < AllArmor.length; fxt++){if(AllArmor[fxt].name.Contains("A_FXTorso_")){FXQ ++;}}
	FXArmor = new GameObject[FXQ];          
	for(var fxu = 0; fxu < FXQ; ++fxu){
		FXArmor[fxu] = GameObject.Find("A_FXTorso_"+fxu+"");}	
					
	// how much armor by categorie
	// Set the quantity number of item for each category by the array length
	headQ = headArmor.Length;
	hairModelQ = hairModel.Length;
	jawQ = jaw.Length;
	eyeQ = eyeAddArmor.Length;
	torsoQ = torsoArmor.Length;
	torsoAddQ = torsoAddArmor.Length;
	beltQ = beltArmor.Length;
	beltAddQ = beltAddArmor.Length;
	shoulderRQ = shoulderRArmor.Length;
	shoulderLQ = shoulderLArmor.Length;
	armRQ = armRArmor.Length;
	armLQ = armLArmor.Length;
	legRQ = legRArmor.Length;
	legLQ = legLArmor.Length;
	weaponRQ = weaponRArmor.Length;
	weaponLQ = weaponLArmor.Length;
	FXQ = FXArmor.Length;

	// Set a selected item for each category(the selected item is the one who will take place on the model it is the displayed model)
	headArmorS = headArmor[headN];
	hairModelS = hairModel[hairModelN];
	jawS = jaw[jawN];
	eyeS = eyeAddArmor[eyeN];
	torsoArmorS = torsoArmor[torsoN];
	torsoAddArmorS = torsoAddArmor[torsoAddN];
	beltArmorS = beltArmor[beltN];
	beltAddArmorS = beltAddArmor[beltAddN];
	shoulderRArmorS = shoulderRArmor[shoulderRN];
	shoulderLArmorS = shoulderLArmor[shoulderLN];
	armRArmorS = armRArmor[armRN];
	armLArmorS = armLArmor[armLN];
	legRArmorS = legRArmor[legRN];
	legLArmorS = legLArmor[legLN];
	weaponRArmorS = weaponRArmor[weaponRN];
	weaponLArmorS = weaponLArmor[weaponLN];
	FXArmorS = FXArmor[FXN];

}


////////////////////////////////////////////////////////   __  ______  ____  ___  ____________////////////////////////////////////////////////////////
////////////////////////////////////////////////////////  / / / / __ \/ __ \/   |/_  __/ ____/////////////////////////////////////////////////////////
//////////////////////////////////////////////////////// / / / / /_/ / / / / /| | / / / __/   ////////////////////////////////////////////////////////
///////////////////////////////////////////////////////// /_/ / ____/ /_/ / ___ |/ / / /___   ////////////////////////////////////////////////////////
////////////////////////////////////////////////////////\____/_/   /_____/_/  |_/_/ /_____/   ////////////////////////////////////////////////////////
////////////////////////////////////////////////////////                                      ////////////////////////////////////////////////////////
function Update () {
	// On click screen
	if (Input.GetButtonDown ("Fire1")){	
				var ray : Ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				var hit : RaycastHit;

		//  _____ _____  _______ _   _ ___ ___ 
		// |_   _| __\ \/ /_   _| | | | _ \ __|
		//   | | | _| >  <  | | | |_| |   / _| 
		//   |_| |___/_/\_\ |_|  \___/|_|_\___|
		                                     
			// if button clik lunch function texture 
			//Color Pilosity
				if (PilosityColorButtonNext.Raycast (ray, hit, 300.0)){colorPilosityNumber++; PilosityColor();} //
				if (PilosityColorButtonBack.Raycast (ray, hit, 300.0)){colorPilosityNumber--; PilosityColor();}  //	
				if (PilosityColorButtonClear.Raycast (ray, hit, 300.0)){colorPilosityNumber =0; PilosityColor();}  //				
			
			//ColorSkin Texture Switch
				if (skinColorButtonNext.Raycast (ray, hit, 300.0)){colorNumber++; ColorSkinF();} //
				if (skinColorButtonBack.Raycast (ray, hit, 300.0)){colorNumber--; ColorSkinF();}  //	
				if (skinColorButtonClear.Raycast (ray, hit, 300.0)){colorNumber =0; ColorSkinF();}  //
			//faceSkin Texture Switch
				if (faceSkinButtonNext.Raycast (ray, hit, 300.0)){skinFaceN++; SkinFaceCombineF();} //
				if (faceSkinButtonBack.Raycast (ray, hit, 300.0)){skinFaceN--; SkinFaceCombineF();}
				if (faceSkinButtonClear.Raycast (ray, hit, 300.0)){skinFaceN=0; SkinFaceCombineF();}
			
			//headAdd Texture Switch	
				if (headAddButtonNext.Raycast (ray, hit, 300.0)){headAddN++; HeadAddCombineF();} //
				if (headAddButtonBack.Raycast (ray, hit, 300.0)){headAddN--; HeadAddCombineF();}
				if (headAddButtonClear.Raycast (ray, hit, 300.0)){headAddN=0; HeadAddCombineF();}

			//EyeBrow Texture Switch	
				if (EyeBrowButtonNext.Raycast (ray, hit, 300.0)){EyeBrowN++; EyeBrowCombineF();} //
				if (EyeBrowButtonBack.Raycast (ray, hit, 300.0)){EyeBrowN--; EyeBrowCombineF();}
				if (EyeBrowButtonClear.Raycast (ray, hit, 300.0)){EyeBrowN=0; EyeBrowCombineF();}
			
			//Scars Texture Switch	
				if (ScarsButtonNext.Raycast (ray, hit, 300.0)){ScarsN++; ScarsCombineF();} //
				if (ScarsButtonBack.Raycast (ray, hit, 300.0)){ScarsN--; ScarsCombineF();}
				if (ScarsButtonClear.Raycast (ray, hit, 300.0)){ScarsN=0; ScarsCombineF();}				
			//Beard Texture Switch	
				if (BeardButtonNext.Raycast (ray, hit, 300.0)){BeardN++; BeardCombineF();} //
				if (BeardButtonBack.Raycast (ray, hit, 300.0)){BeardN--; BeardCombineF();}
				if (BeardButtonClear.Raycast (ray, hit, 300.0)){BeardN=0; BeardCombineF();}		
			
			//HairSkull Texture Switch	
				if (HairSkullButtonNext.Raycast (ray, hit, 300.0)){HairSkullN++; HairSkullCombineF();} //
				if (HairSkullButtonBack.Raycast (ray, hit, 300.0)){HairSkullN--; HairSkullCombineF();}	
				if (HairSkullButtonClear.Raycast (ray, hit, 300.0)){HairSkullN=0; HairSkullCombineF();}
			//Eye Texture Switch	
				if (EyeButtonNext.Raycast (ray, hit, 300.0)){EyeN++; EyeCombineF();} //
				if (EyeButtonBack.Raycast (ray, hit, 300.0)){EyeN--; EyeCombineF();}
				if (EyeButtonClear.Raycast (ray, hit, 300.0)){EyeN=0; EyeCombineF();}

			//Pant Texture Switch	
				if (PantButtonNext.Raycast (ray, hit, 300.0)){PantN++; PantCombineF();} //
				if (PantButtonBack.Raycast (ray, hit, 300.0)){PantN--; PantCombineF();}
				if (PantButtonClear.Raycast (ray, hit, 300.0)){PantN=0; PantCombineF();}
				
			//Torso Texture Switch	
				if (TorsoButtonNext.Raycast (ray, hit, 300.0)){TorsoN++; TorsoCombineF();} //
				if (TorsoButtonBack.Raycast (ray, hit, 300.0)){TorsoN--; TorsoCombineF();}
				if (TorsoButtonClear.Raycast (ray, hit, 300.0)){TorsoN=0; TorsoCombineF();}	
			
			//Shoe Texture Switch	
				if (ShoeButtonNext.Raycast (ray, hit, 300.0)){ShoeN++; ShoeCombineF();} //
				if (ShoeButtonBack.Raycast (ray, hit, 300.0)){ShoeN--; ShoeCombineF();}	
				if (ShoeButtonClear.Raycast (ray, hit, 300.0)){ShoeN=0; ShoeCombineF();}		

			//Glove Texture Switch	
				if (GloveButtonNext.Raycast (ray, hit, 300.0)){GloveN++; GloveCombineF();} //
				if (GloveButtonBack.Raycast (ray, hit, 300.0)){GloveN--; GloveCombineF();}	
				if (GloveButtonClear.Raycast (ray, hit, 300.0)){GloveN=0; GloveCombineF();}
			
			//Belt Texture Switch	
				if (BeltButtonNext.Raycast (ray, hit, 300.0)){BeltN++; BeltCombineF();} //
				if (BeltButtonBack.Raycast (ray, hit, 300.0)){BeltN--; BeltCombineF();}
				if (BeltButtonClear.Raycast (ray, hit, 300.0)){BeltN=0; BeltCombineF();}	
			
			// random tunique 
			    if (RandomTuniqueButton.Raycast (ray, hit, 300.0)){
			   randTunique = Random.Range(1,11);
			   		PantN = randTunique;
			   		TorsoN = randTunique;
			   		ShoeN = randTunique;
			   		robeLongN = randTunique;
			   		robeShortN = randTunique;
			   		GloveN = randTunique;
			   		BeltN = randTunique;
			       PantCombineF();
			     }
			     // Random Face
			     if(RandomFaceButton.Raycast (ray, hit, 300.0)){
			   		ScarsN = Random.Range(0,ScarsQ);
					EyeBrowN = Random.Range(0,EyeBrowQ);
					skinFaceN = Random.Range(0,skinFaceQ);
					colorNumber = Random.Range(0,5);
					colorPilosityNumber = Random.Range(0,4);
					BeardN = Random.Range(0,BeardQ);
					HairSkullN = Random.Range(0,HairSkullQ);
					//headAddN =Random.Range(0,headAddQ);
					EyeN =Random.Range(0,EyeQ);
			     	 PilosityColor();
			       }
			// clear texture set all to default texture 0	
				if (clearAllButton.Raycast (ray, hit, 300.0)||clearTex1.Raycast (ray, hit, 300.0)){
																BeltN=0;
															    GloveN=0;
															    ShoeN=0;
															    TorsoN=0;
															    PantN=0;
															    EyeN=0;
															    HairSkullN=0;
															    BeardN=0;
															    ScarsN=0;
															    EyeBrowN=0;
															    headAddN=0;
															    skinFaceN=0;
															    colorNumber =0;
															    ColorSkinF();}				
				
				
												
		
		//    _   ___ __  __  ___  ___ 
		//   /_\ | _ \  \/  |/ _ \| _ \
		//  / _ \|   / |\/| | (_) |   /
		// /_/ \_\_|_\_|  |_|\___/|_|_\
                             
				
				 		// Unequip selected item from model;
				 		// Increment value for the next selected item ;
				 		// set the boolean to true if an item is equiped
				 		// Lunch equip the selected item function;
				 		
		//Head 0
				
		 		if (head_next.Raycast (ray, hit, 300.0)){headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;headN++;HeadEquip();ArmorpartEquip[0] = true;FBTexArmor ();}
		 		if (head_back.Raycast (ray, hit, 300.0)){headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;headN--;HeadEquip();ArmorpartEquip[0] = true;FBTexArmor ();}
		 		if (head_clear.Raycast (ray, hit, 300.0) || clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHair;ArmorpartEquip[0] = false;equipedArmor[0] = null;FBTexArmor ();}
		//HairModel 1
		 		if (hairModel_next.Raycast (ray, hit, 300.0)){hairModelS.transform.position = anchorHair.position;hairModelS.transform.parent = anchorHair;hairModelN++;hairModelEquip();ArmorpartEquip[1] = true;FBTexArmor ();}
		 		if (hairModel_back.Raycast (ray, hit, 300.0)){hairModelS.transform.position = anchorHair.position;hairModelS.transform.parent = anchorHair;hairModelN--;hairModelEquip();ArmorpartEquip[1] = true;FBTexArmor ();}
		 		if (hairModel_clear.Raycast (ray, hit, 300.0) || clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){hairModelS.transform.position = anchorHair.position;hairModelS.transform.parent = anchorHair;ArmorpartEquip[1] = false;equipedArmor[1] = null;FBTexArmor ();}
		//jaw 2
		 		if (jaw_next.Raycast (ray, hit, 300.0)){jawS.transform.position = anchorJaw.position;jawS.transform.parent = anchorJaw;jawN++;JawEquip();ArmorpartEquip[2] = true;FBTexArmor ();}
		 		if (jaw_back.Raycast (ray, hit, 300.0)){jawS.transform.position = anchorJaw.position;jawS.transform.parent = anchorJaw;jawN--;JawEquip();ArmorpartEquip[2] = true;FBTexArmor ();}
		 		if (jaw_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){jawS.transform.position = anchorJaw.position;jawS.transform.parent = anchorJaw;ArmorpartEquip[2] = false;equipedArmor[2] = null;FBTexArmor ();}		
		//Torso 3
		 		if (torso_next.Raycast (ray, hit, 300.0)){torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;torsoN++;TorsoEquip();ArmorpartEquip[3] = true;FBTexArmor ();}
		 		if (torso_back.Raycast (ray, hit, 300.0)){torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;torsoN--;TorsoEquip();ArmorpartEquip[3] = true;FBTexArmor ();}
		 		if (torso_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;ArmorpartEquip[3] = false;equipedArmor[3] = null;FBTexArmor ();}	
		//TorsoAdd 4
		 		if (torsoAdd_next.Raycast (ray, hit, 300.0)){torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;torsoAddN++;TorsoAddEquip();ArmorpartEquip[4] = true;FBTexArmor ();}
		 		if (torsoAdd_back.Raycast (ray, hit, 300.0)){torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;torsoAddN--;TorsoAddEquip();ArmorpartEquip[4] = true;FBTexArmor ();}
		 		if (torsoAdd_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;ArmorpartEquip[4] = false;equipedArmor[4] = null;FBTexArmor ();}		 		 			 		
		//Belt 5			
		 		if (belt_next.Raycast (ray, hit, 300.0)){beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;beltN++;BeltEquip();ArmorpartEquip[5] = true;FBTexArmor ();}
		 		if (belt_back.Raycast (ray, hit, 300.0)){beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;beltN--;BeltEquip();ArmorpartEquip[5] = true;FBTexArmor ();}
		 		if (belt_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;ArmorpartEquip[5] = false;equipedArmor[5] = null;FBTexArmor ();}		 		
		//BeltAdd 6		
		 		if (beltAdd_next.Raycast (ray, hit, 300.0)){beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;beltAddN++;BeltAddEquip();ArmorpartEquip[6] = true;FBTexArmor ();}
		 		if (beltAdd_back.Raycast (ray, hit, 300.0)){beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;beltAddN--;BeltAddEquip();ArmorpartEquip[6] = true;FBTexArmor ();}
		 		if (beltAdd_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;ArmorpartEquip[6] = false;equipedArmor[6] = null;FBTexArmor ();}		 				 				 		
		//ShoulderR 7  		
		 		if (shoulderR_next.Raycast (ray, hit, 300.0)){shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;shoulderRN++;ShoulderREquip();ArmorpartEquip[7] = true;FBTexArmor ();}
		 		if (shoulderR_back.Raycast (ray, hit, 300.0)){shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;shoulderRN--;ShoulderREquip();ArmorpartEquip[7] = true;FBTexArmor ();}
		 		if (shoulderR_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;ArmorpartEquip[7] = false;equipedArmor[7] = null;FBTexArmor ();}		 			 				 				
		//ShoulderL	8
		 		if (shoulderL_next.Raycast (ray, hit, 300.0)){shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;shoulderLN++;ShoulderLEquip();ArmorpartEquip[8] = true;FBTexArmor ();}
		 		if (shoulderL_back.Raycast (ray, hit, 300.0)){shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;shoulderLN--;ShoulderLEquip();ArmorpartEquip[8] = true;FBTexArmor ();}
		 		if (shoulderL_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;ArmorpartEquip[8] = false;equipedArmor[8] = null;FBTexArmor ();}		 		
		//ArmR 9		
		 		if (armR_next.Raycast (ray, hit, 300.0)){armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;armRN++;ArmREquip();ArmorpartEquip[9] = true;FBTexArmor ();}
		 		if (armR_back.Raycast (ray, hit, 300.0)){armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;armRN--;ArmREquip();ArmorpartEquip[9] = true;FBTexArmor ();}
		 		if (armR_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;ArmorpartEquip[9] = false;equipedArmor[9] = null;FBTexArmor ();}	 				
		//ArmL 10	
		 		if (armL_next.Raycast (ray, hit, 300.0)){armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;armLN++;ArmLEquip();ArmorpartEquip[10] = true;FBTexArmor ();}
		 		if (armL_back.Raycast (ray, hit, 300.0)){armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;armLN--;ArmLEquip();ArmorpartEquip[10] = true;FBTexArmor ();}
		 		if (armL_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;ArmorpartEquip[10] = false;equipedArmor[10] = null;FBTexArmor ();}
		//LegR 11
		 		if (legR_next.Raycast (ray, hit, 300.0)){legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;legRN++;LegREquip();ArmorpartEquip[11] = true;FBTexArmor ();}
		 		if (legR_back.Raycast (ray, hit, 300.0)){legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;legRN--;LegREquip();ArmorpartEquip[11] = true;FBTexArmor ();}
		 		if (legR_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegL;ArmorpartEquip[11] = false;equipedArmor[11] = null;FBTexArmor ();}		
		// LegL 12
		 		if (legL_next.Raycast (ray, hit, 300.0)){legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;legLN++;LegLEquip();ArmorpartEquip[12] = true;FBTexArmor ();}
		 		if (legL_back.Raycast (ray, hit, 300.0)){legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;legLN--;LegLEquip();ArmorpartEquip[12] = true;FBTexArmor ();}
		 		if (legL_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;ArmorpartEquip[12] = false;equipedArmor[12] = null;FBTexArmor ();}				 	
		// WeaponR 13
		 		if (weaponR_next.Raycast (ray, hit, 300.0)){weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;weaponRN++;WeaponREquip();ArmorpartEquip[13] = true;FBTexArmor ();}
		 		if (weaponR_back.Raycast (ray, hit, 300.0)){weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;weaponRN--;WeaponREquip();ArmorpartEquip[13] = true;FBTexArmor ();}
		 		if (weaponR_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;ArmorpartEquip[13] = false;equipedArmor[13] = null;FBTexArmor ();}		
		// WeaponL	14
		 		if (weaponL_next.Raycast (ray, hit, 300.0)){weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;weaponLN++;WeaponLEquip();ArmorpartEquip[14] = true;FBTexArmor ();}
		 		if (weaponL_back.Raycast (ray, hit, 300.0)){weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;weaponLN--;WeaponLEquip();ArmorpartEquip[14] = true;FBTexArmor ();}
		 		if (weaponL_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)|| clearTex2.Raycast (ray, hit, 300.0)){weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;ArmorpartEquip[14] = false;equipedArmor[14] = null;FBTexArmor ();}						
		//FX
		 		if (FX_next.Raycast (ray, hit, 300.0)){FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;FXN++;FXEquip();FBTexArmor ();}
		 		if (FX_back.Raycast (ray, hit, 300.0)){FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;FXN--;FXEquip();FBTexArmor ();}
		 		if (FX_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)){FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;FBTexArmor ();}
		//EyeFx
		 		if (eye_next.Raycast (ray, hit, 300.0)){eyeS.transform.position = anchorEye.position;eyeS.transform.parent = anchorEye;eyeN++;EyeEquip();FBTexArmor ();}
		 		if (eye_back.Raycast (ray, hit, 300.0)){eyeS.transform.position = anchorEye.position;eyeS.transform.parent = anchorEye;eyeN--;EyeEquip();FBTexArmor ();}
		 		if (eye_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)){eyeS.transform.position = anchorEye.position;eyeS.transform.parent = anchorEye;FBTexArmor ();}
		//Cloak 
				if (cloak_next.Raycast (ray, hit, 300.0)){cloakN++;CloakOn();}
		 		if (cloak_back.Raycast (ray, hit, 300.0)){cloakN--;CloakOn();}
		 		if (cloak_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)){CloakOff();}
		//RobeLong
				if (robeLong_next.Raycast (ray, hit, 300.0)){robeLongN++;RobeLongOn();robeLongB=true;RobeLongCombine();}
		 		if (robeLong_back.Raycast (ray, hit, 300.0)){robeLongN--;RobeLongOn();robeLongB=true;RobeLongCombine();}
		 		if (robeLong_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)){RobeLongOff();robeLongB=false;RobeLongCombine();}
		//RobeShort
				if (robeShort_next.Raycast (ray, hit, 300.0)){robeShortN++;RobeShortOn();robeShortB=true;RobeShortCombine();}
		 		if (robeShort_back.Raycast (ray, hit, 300.0)){robeShortN--;RobeShortOn();robeShortB=true;RobeShortCombine();}
		 		if (robeShort_clear.Raycast (ray, hit, 300.0)|| clearAllButton.Raycast (ray, hit, 300.0)){RobeShortOff();robeShortB=false;RobeShortCombine();}
				
				
				
				
				// RANDOM BUTTON 
				
				if (randomButton.Raycast (ray, hit, 300.0)){
					
					//remove all armor part from the model
					
					headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;
					hairModelS.transform.position = anchorHair.position;hairModelS.transform.parent = anchorHair;
					jawS.transform.position = anchorJaw.position;jawS.transform.parent = anchorJaw;
					eyeS.transform.position = anchorEye.position;eyeS.transform.parent = anchorEye;
					torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;
					torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;
					beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;
					beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;
					shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;
					shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;
					armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;
					armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;
					legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;
					legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;
					weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;
					weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;
					FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;
					
					// Random texture Skin
					// set the random range on each category of texture
					// - 5 because that make more chance no item is added to the model (you can modify this number to modify the randomness)
					
					ScarsN = Random.Range(0,ScarsQ);
					EyeBrowN = Random.Range(0,EyeBrowQ);
					skinFaceN = Random.Range(0,skinFaceQ);
					colorNumber = Random.Range(0,5);
					colorPilosityNumber = Random.Range(0,4);
					BeardN = Random.Range(0,BeardQ);
					HairSkullN = Random.Range(0,HairSkullQ);
					//headAddN =Random.Range(0,headAddQ);
					EyeN =Random.Range(0,EyeQ);
					PantN =Random.Range(0,PantQ);
					TorsoN =Random.Range(0,TorsoQ);
					ShoeN =Random.Range(0,ShoeQ);
					GloveN =Random.Range(0,GloveQ);
					BeltN =Random.Range(0,BeltQ);
					robeLongN = Random.Range(-5,robeLongQ);
					if(robeLongN >= 1){robeLongB=true;RobeLongOn();}
					if(robeLongN <= 0){robeLongN=0;robeLongB=false;RobeLongOff();}
					robeShortN = Random.Range(-5,robeShortQ);
					if(robeShortN >= 1){robeShortB=true;RobeShortOn();}
					if(robeShortN <= 0){robeShortN=0;robeShortB=false;RobeShortOff();}				
					
					
					//Random armor mesh
					// set the number of the selected item to be placed on the model
					
					hairModelN = Random.Range(-5,hairModelQ);
					headN = Random.Range(-5,headQ);	
					jawN = Random.Range(-1,jawQ);
					eyeN = Random.Range(-20,eyeQ);
					torsoN = Random.Range(-5,torsoQ);
					torsoAddN = Random.Range(-5,torsoAddQ);
					beltN = Random.Range(-5,beltQ);
					beltAddN = Random.Range(-5,beltAddQ);
					shoulderRN = Random.Range(-5,shoulderRQ);
					shoulderLN = shoulderRN;
					armRN = Random.Range(-5,armRQ);
					armLN = armRN;
					legRN = Random.Range(-5,legRQ);
					legLN = legRN;
					weaponRN = Random.Range(-5,weaponRQ);
					weaponLN = Random.Range(-5,weaponLQ);
					//FXN = Random.Range(-5,FXQ); // enable it if you want FxTorso be part of the random
					
					
					//lunch pilosity function to remap the color on the mesh hair and the texture color
					PilosityColor ();
					
					// set all the boolean to false (no item is equiped)
					ArmorpartEquip[0] = false;
					ArmorpartEquip[1] = false;
					ArmorpartEquip[2] = false;
					ArmorpartEquip[3] = false;
					ArmorpartEquip[4] = false;
					ArmorpartEquip[5] = false;
					ArmorpartEquip[6] = false;
					ArmorpartEquip[7] = false;
					ArmorpartEquip[8] = false;
					ArmorpartEquip[9] = false;
					ArmorpartEquip[10] = false;
					ArmorpartEquip[11] = false;
					ArmorpartEquip[12] = false;
					ArmorpartEquip[13] = false;
					ArmorpartEquip[14] = false;
					
					//lunch texture feedback armor function 
					FBTexArmor ();
					
					// IF ITEM SELECTED IS DIFFERENT FROM 0 LUNCH EQUIP FUNCTION		OTHERWISE//set boolean of equiped item to false, remove item from model equiped armor array, lunch feedback texture armor function,remove armor part from the model
					if(headN>=0){HeadEquip();ArmorpartEquip[0] = true;} 			else {ArmorpartEquip[0] = false;equipedArmor[0] = null;FBTexArmor ();headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;}
					if(hairModelN>=0){hairModelEquip();ArmorpartEquip[1] = true;} 	else {ArmorpartEquip[1] = false;equipedArmor[1] = null;FBTexArmor ();hairModelS.transform.position = anchorHair.position;hairModelS.transform.parent = anchorHead;}
					if(jawN>=0){JawEquip();ArmorpartEquip[2] = true;} 				else {ArmorpartEquip[2] = false;equipedArmor[2] = null;FBTexArmor ();jawS.transform.position = anchorJaw.position; jawS.transform.parent = anchorJaw; }
					if(torsoN>=0){TorsoEquip();ArmorpartEquip[3] = true;} 			else {ArmorpartEquip[3] = false;equipedArmor[3] = null;FBTexArmor ();torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;}
					if(torsoAddN>=0){TorsoAddEquip();ArmorpartEquip[4] = true;} 	else {ArmorpartEquip[4] = false;equipedArmor[4] = null;FBTexArmor ();torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;}
					if(beltN>=0){BeltEquip();ArmorpartEquip[5] = true;} 			else {ArmorpartEquip[5] = false;equipedArmor[5] = null;FBTexArmor ();beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;}
					if(beltAddN>=0){BeltAddEquip();ArmorpartEquip[6] = true;} 		else {ArmorpartEquip[6] = false;equipedArmor[6] = null;FBTexArmor ();beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;}
					if(armLN>=0){ArmLEquip();ArmorpartEquip[10] = true;} 			else {ArmorpartEquip[10] = false;equipedArmor[10] = null;FBTexArmor ();armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;}
					if(armRN>=0){ArmREquip();ArmorpartEquip[9] = true;}				else {ArmorpartEquip[9] = false;equipedArmor[9] = null;FBTexArmor ();armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;}
					if(legRN>=0){LegREquip();ArmorpartEquip[11] = true;}			else {ArmorpartEquip[11] = false;equipedArmor[11] = null;FBTexArmor ();legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;}
					if(legLN>=0){LegLEquip();ArmorpartEquip[12] = true;} 			else {ArmorpartEquip[12] = false;equipedArmor[12] = null;FBTexArmor ();legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;}
					if(shoulderRN>=0){ShoulderREquip();ArmorpartEquip[7] = true;}  else {ArmorpartEquip[7] = false;equipedArmor[7] = null;FBTexArmor ();shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;}
					if(shoulderLN>=0){ShoulderLEquip();ArmorpartEquip[8] = true;}  else {ArmorpartEquip[8] = false;equipedArmor[8] = null;FBTexArmor ();shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;}
					if(weaponLN>=0){WeaponLEquip();ArmorpartEquip[14] = true;} 		else {ArmorpartEquip[14] = false;equipedArmor[14] = null;FBTexArmor ();weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;}
					if(weaponRN>=0){WeaponREquip();ArmorpartEquip[13] = true;} 		else {ArmorpartEquip[13] = false;equipedArmor[13] = null;FBTexArmor ();weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;}
					
					//if(FXN>=0){FXEquip();} else {FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;} if you want activate the fx in the randomness
					if(eyeN>=0){EyeEquip();} else {eyeS.transform.position = anchorEye.position; eyeS.transform.parent = anchorEye; }
					
					// lunch feedback texture armor
					FBTexArmor ();
					}
				
				
				
				
				
				if (randomButtonTex1.Raycast (ray, hit, 300.0)){
					// random texture skin
					
					ScarsN = Random.Range(0,ScarsQ);
					EyeBrowN = Random.Range(0,EyeBrowQ);
					skinFaceN = Random.Range(0,skinFaceQ);
					colorNumber = Random.Range(0,5);
					colorPilosityNumber = Random.Range(0,3);
					BeardN = Random.Range(0,BeardQ);
					HairSkullN = Random.Range(0,HairSkullQ);
					//headAddN =Random.Range(0,headAddQ);
					EyeN =Random.Range(0,EyeQ);
					PantN =Random.Range(0,PantQ);
					TorsoN =Random.Range(0,TorsoQ);
					ShoeN =Random.Range(0,ShoeQ);
					GloveN =Random.Range(0,GloveQ);
					BeltN =Random.Range(0,BeltQ);
					
					robeLongN = Random.Range(-5,robeLongQ);
					if(robeLongN >= 1){robeLongB=true;RobeLongOn();}
					if(robeLongN <= 0){robeLongN=0;robeLongB=false;RobeLongOff();}
					robeShortN = Random.Range(-5,robeShortQ);
					if(robeShortN >= 1){robeShortB=true;RobeShortOn();}
					if(robeShortN <= 0){robeShortN=0;robeShortB=false;RobeShortOff();}
					
					PilosityColor ();

					}

//RANDOM BUTTON //Randomly add stuff to the character
				if (randomButtonTex2.Raycast (ray, hit, 300.0)){
					
					

					//remove all armor part
					
					headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;
					hairModelS.transform.position = anchorHead.position;hairModelS.transform.parent = anchorHead;
					jawS.transform.position = anchorJaw.position;jawS.transform.parent = anchorJaw;
					eyeS.transform.position = anchorEye.position;eyeS.transform.parent = anchorEye;
					torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;
					torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;
					beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;
					beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;
					shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;
					shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;
					armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;
					armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;
					legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;
					legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;
					weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;
					weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;
					FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;
					
					// - 5 because that make more chance no item is added to the model (you can modify this number to modify the randomness)
					
					//Random mesh armor
					
					hairModelN = Random.Range(-5,hairModelQ);
					
					if(hairModelN > -1){headN=-1;}else{
					headN = Random.Range(-5,headQ);}
					
						
					jawN = Random.Range(-1,jawQ);
					eyeN = Random.Range(-20,eyeQ);
					torsoN = Random.Range(-5,torsoQ);
					torsoAddN = Random.Range(-5,torsoAddQ);
					beltN = Random.Range(-5,beltQ);
					beltAddN = Random.Range(-5,beltAddQ);
					shoulderRN = Random.Range(-5,shoulderRQ);
					shoulderLN = shoulderRN;
					armRN = Random.Range(-5,armRQ);
					armLN = armRN;
					legRN = Random.Range(-5,legRQ);
					legLN = legRN;
					weaponRN = Random.Range(-5,weaponRQ);
					weaponLN = Random.Range(-5,weaponLQ);
					//FXN = Random.Range(-5,FXQ);

					
					ArmorpartEquip[0] = false;
					ArmorpartEquip[1] = false;
					ArmorpartEquip[2] = false;
					ArmorpartEquip[3] = false;
					ArmorpartEquip[4] = false;
					ArmorpartEquip[5] = false;
					ArmorpartEquip[6] = false;
					ArmorpartEquip[7] = false;
					ArmorpartEquip[8] = false;
					ArmorpartEquip[9] = false;
					ArmorpartEquip[10] = false;
					ArmorpartEquip[11] = false;
					ArmorpartEquip[12] = false;
					ArmorpartEquip[13] = false;
					ArmorpartEquip[14] = false;
					
					
					FBTexArmor ();
					
					if(headN>=0){HeadEquip();ArmorpartEquip[0] = true;} 			else {ArmorpartEquip[0] = false;equipedArmor[0] = null;FBTexArmor ();headArmorS.transform.position = anchorHead.position;headArmorS.transform.parent = anchorHead;}
					if(hairModelN>=0){hairModelEquip();ArmorpartEquip[1] = true;} 	else {ArmorpartEquip[1] = false;equipedArmor[1] = null;FBTexArmor ();hairModelS.transform.position = anchorHead.position;hairModelS.transform.parent = anchorHead;}
					if(jawN>=0){JawEquip();ArmorpartEquip[2] = true;} 				else {ArmorpartEquip[2] = false;equipedArmor[2] = null;FBTexArmor ();jawS.transform.position = anchorJaw.position; jawS.transform.parent = anchorJaw; }
					if(torsoN>=0){TorsoEquip();ArmorpartEquip[3] = true;} 			else {ArmorpartEquip[3] = false;equipedArmor[3] = null;FBTexArmor ();torsoArmorS.transform.position = anchorTorso.position;torsoArmorS.transform.parent = anchorTorso;}
					if(torsoAddN>=0){TorsoAddEquip();ArmorpartEquip[4] = true;} 	else {ArmorpartEquip[4] = false;equipedArmor[4] = null;FBTexArmor ();torsoAddArmorS.transform.position = anchorTorso.position;torsoAddArmorS.transform.parent = anchorTorso;}
					if(beltN>=0){BeltEquip();ArmorpartEquip[5] = true;} 			else {ArmorpartEquip[5] = false;equipedArmor[5] = null;FBTexArmor ();beltArmorS.transform.position = anchorBelt.position;beltArmorS.transform.parent = anchorBelt;}
					if(beltAddN>=0){BeltAddEquip();ArmorpartEquip[6] = true;} 		else {ArmorpartEquip[6] = false;equipedArmor[6] = null;FBTexArmor ();beltAddArmorS.transform.position = anchorBelt.position;beltAddArmorS.transform.parent = anchorBelt;}
					if(armLN>=0){ArmLEquip();ArmorpartEquip[10] = true;} 			else {ArmorpartEquip[10] = false;equipedArmor[10] = null;FBTexArmor ();armLArmorS.transform.position = anchorArmL.position;armLArmorS.transform.parent = anchorArmL;}
					if(armRN>=0){ArmREquip();ArmorpartEquip[9] = true;}				else {ArmorpartEquip[9] = false;equipedArmor[9] = null;FBTexArmor ();armRArmorS.transform.position = anchorArmR.position;armRArmorS.transform.parent = anchorArmR;}
					if(legRN>=0){LegREquip();ArmorpartEquip[11] = true;}			else {ArmorpartEquip[11] = false;equipedArmor[11] = null;FBTexArmor ();legRArmorS.transform.position = anchorLegR.position;legRArmorS.transform.parent = anchorLegR;}
					if(legLN>=0){LegLEquip();ArmorpartEquip[12] = true;} 			else {ArmorpartEquip[12] = false;equipedArmor[12] = null;FBTexArmor ();legLArmorS.transform.position = anchorLegL.position;legLArmorS.transform.parent = anchorLegL;}
					if(shoulderRN>=0){ShoulderREquip();ArmorpartEquip[7] = true;}  else {ArmorpartEquip[7] = false;equipedArmor[7] = null;FBTexArmor ();shoulderRArmorS.transform.position = anchorShoulderR.position;shoulderRArmorS.transform.parent = anchorShoulderR;}
					if(shoulderLN>=0){ShoulderLEquip();ArmorpartEquip[8] = true;}  else {ArmorpartEquip[8] = false;equipedArmor[8] = null;FBTexArmor ();shoulderLArmorS.transform.position = anchorShoulderL.position;shoulderLArmorS.transform.parent = anchorShoulderL;}
					if(weaponLN>=0){WeaponLEquip();ArmorpartEquip[14] = true;} 		else {ArmorpartEquip[14] = false;equipedArmor[14] = null;FBTexArmor ();weaponLArmorS.transform.position = anchorWeaponL.position;weaponLArmorS.transform.parent = anchorWeaponL;}
					if(weaponRN>=0){WeaponREquip();ArmorpartEquip[13] = true;} 		else {ArmorpartEquip[13] = false;equipedArmor[13] = null;FBTexArmor ();weaponRArmorS.transform.position = anchorWeaponR.position;weaponRArmorS.transform.parent = anchorWeaponR;}
					
					//if(FXN>=0){FXEquip();} else {FXArmorS.transform.position = anchorFX.position;FXArmorS.transform.parent = anchorFX;}
					if(eyeN>=0){EyeEquip();} else {eyeS.transform.position = anchorHead.position; eyeS.transform.parent = anchorHead; }
					
					FBTexArmor ();
				}

			
			//	create prefab
				if (savePrefabButton.Raycast (ray, hit, 300.0)){MakePrefab();}

	}
		
}
////////////////////////////////////////////////////////////////////  _____________  __________  ______  ______
//////////////////////////////////////////////////////////////////// /_  __/ ____/ |/ /_  __/ / / / __ \/ ____/////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////  / / / __/  |   / / / / / / / /_/ / __/   ////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////// / / / /___ /   | / / / /_/ / _, _/ /___   ////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////_/ /_____//_/|_|/_/  \____/_/ |_/_____/   ////////////////////////////////////////////////////////////////////

// this function reload color texture in folder for matching color hair 
function PilosityColor(){
		if(colorPilosityNumber <= -1){colorPilosityNumber = 3;} //check color number
		if(colorPilosityNumber >= 4){colorPilosityNumber = 0;}	//fix color number if max
		if(colorPilosityNumber >=0 && colorPilosityNumber <= 3){ //if color number match 
	
// EyeBrow Texture in folder 	
	EyeBrow = new Texture2D[EyeBrowSearch.length];                                                              								//resize array
	for(var cP = 0; cP < EyeBrowSearch.Length; ++cP){																      						   //fill with Texture2D
		EyeBrow[cP] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Eyebrow/Color"+colorPilosityNumber+"/EyeBrow" +cP+ "_Color"+colorPilosityNumber+".png", Texture2D) as Texture2D;}
    EyeBrowQ = EyeBrow.Length;  
// Beard Texture in folder  
	Beard = new Texture2D[BeardSearch.length];                                                              								//resize array
	for(var eP = 0; eP < BeardSearch.Length; ++eP){																      						   //fill with Texture2D
		Beard[eP] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Beard/Color"+colorPilosityNumber+"/Beard" +eP+ "_Color"+colorPilosityNumber+".png", Texture2D) as Texture2D;}
    BeardQ = Beard.Length;  
// HairSkull Texture in folder  
	HairSkull = new Texture2D[HairSkullSearch.length];                                                              								//resize array
	for(var gP = 0; gP < HairSkullSearch.Length; ++gP){																      						   //fill with Texture2D
		HairSkull[gP] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_HairSkull/Color"+colorPilosityNumber+"/HumanMale_HairSkull" +gP+ "_Color"+colorPilosityNumber+".png", Texture2D) as Texture2D;}
    HairSkullQ = HairSkull.Length;  
		
		ColorSkinF ();
		if(ArmorpartEquip[1] == true ){hairModelEquip();}
		if(ArmorpartEquip[2] == true ){JawEquip();}
		}
		
}                                           
                                                                                      
   // those function texture are a cascade of function, from color skin to belt every texture are aplied   // each time you lunch a function it start the following texure function
                                                                                                                                                                      
function ColorSkinF () {
		if(colorNumber <= -1){colorNumber = 5;}
		if(colorNumber >= 6){colorNumber = 0;}
		if(colorNumber >=0 && colorNumber <= 5){
			
	atlasBaseSkinFace = new Texture2D[atlasBaseSearchSkinFace.length];
	for(var a = 0; a < atlasBaseSearchSkinFace.Length; ++a){
		atlasBaseSkinFace[a] = AssetDatabase.LoadAssetAtPath(
		"Assets/Character_Editor/Textures/Character/HumanMale/HumanMale_Color/Color"+colorNumber+"/HumanMale_Color"+colorNumber+"_FaceSkin" +a+ ".png", Texture2D) as Texture2D;}
	skinFaceQ = atlasBaseSkinFace.Length;
		
			skinFaceS = atlasBaseSkinFace[skinFaceN];	
			atlasBase = skinFaceS;
			ColorSkinCombine = new Texture2D(atlasBase.width, atlasBase.height);
			ColorSkinCombine.Apply();
			SkinFaceCombineF ();
		}
}

function SkinFaceCombineF () {
		if(skinFaceN <= -1){skinFaceN = skinFaceQ - 1;}
		if(skinFaceN >= skinFaceQ){skinFaceN = 0;}
		if(skinFaceN >=0 && skinFaceN <= skinFaceQ - 1){	
			
			skinFaceS = atlasBaseSkinFace[skinFaceN];
			atlasBase = skinFaceS;
			CombineAll = atlasBase;			
			CombineAll.Apply();
			EyeBrowCombineF ();
		}
}

function EyeBrowCombineF () {
		if(EyeBrowN <= -1){EyeBrowN = EyeBrowQ - 1;}
		if(EyeBrowN >= EyeBrowQ){EyeBrowN = 0;}
		if(EyeBrowN >=0 && EyeBrowN <= EyeBrowQ - 1){	
			atlasBase = skinFaceS;
			var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			var pixelBaseHead =  atlasBase.GetPixels(384,0,640,384);    	   // texture head base baseHead.GetPixels32();
			var	pixelEyeBrow = EyeBrow[EyeBrowN].GetPixels32();   // Selected EyeBrow texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelEyeBrow[p].a != pixelBaseHead[p].a && pixelEyeBrow[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelEyeBrow[p],Time.fixedDeltaTime * pixelEyeBrow[p].a);
					}
			}
		
			EyeBrowCombine = new Texture2D(atlasBase.width, atlasBase.height);
			EyeBrowCombine.SetPixels(pixelAtlasBase);
			EyeBrowCombine.SetPixels(384,0,640,384,pixelBaseHead);
			EyeBrowCombine.Apply();
			ScarsCombineF ();
		}
}

function ScarsCombineF () {
		if(ScarsN <= -1){ScarsN = ScarsQ - 1;}
		if(ScarsN >= ScarsQ){ScarsN = 0;}
		if(ScarsN >=0 && ScarsN <= ScarsQ - 1){	
			atlasBase = EyeBrowCombine;
			var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			var pixelBaseHead =  atlasBase.GetPixels(384,0,640,384);    	   // texture head base baseHead.GetPixels32();
			var	pixelScars = Scars[ScarsN].GetPixels32();   // Selected Scars texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelScars[p].a != pixelBaseHead[p].a && pixelScars[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelScars[p],Time.fixedDeltaTime * pixelScars[p].a);
					}
			}
			
			ScarsCombine = new Texture2D(atlasBase.width, atlasBase.height);
			ScarsCombine.SetPixels(pixelAtlasBase);
			ScarsCombine.SetPixels(384,0,640,384,pixelBaseHead);
			ScarsCombine.Apply();
			BeardCombineF ();
			
		}
}
function BeardCombineF () {
		if(BeardN <= -1){BeardN = BeardQ - 1;}
		if(BeardN >= BeardQ){BeardN = 0;}
		if(BeardN >=0 && BeardN <= BeardQ - 1){	
			
			atlasBase = ScarsCombine;
			var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			var pixelBaseHead =  atlasBase.GetPixels(384,0,640,384);    	   // texture head base baseHead.GetPixels32();
			var	pixelBeard = Beard[BeardN].GetPixels32();   // Selected Beard texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelBeard[p].a != pixelBaseHead[p].a && pixelBeard[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelBeard[p],Time.fixedDeltaTime * pixelBeard[p].a);
					}
			}
			
			BeardCombine = new Texture2D(atlasBase.width, atlasBase.height);
			BeardCombine.SetPixels(pixelAtlasBase);
			BeardCombine.SetPixels(384,0,640,384,pixelBaseHead);
			
			BeardCombine.Apply();
			HairSkullCombineF ();
		}
}
function HairSkullCombineF () {
		if(HairSkullN <= -1){HairSkullN = HairSkullQ - 1;}
		if(HairSkullN >= HairSkullQ){HairSkullN = 0;}
		if(HairSkullN >=0 && HairSkullN <= HairSkullQ - 1){	
			
			atlasBase = BeardCombine;
			var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			var pixelBaseHead =  atlasBase.GetPixels(384,0,640,384);    	   // texture head base baseHead.GetPixels32();
			var	pixelHairSkull = HairSkull[HairSkullN].GetPixels32();   // Selected HairSkull texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelHairSkull[p].a != pixelBaseHead[p].a && pixelHairSkull[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelHairSkull[p],Time.fixedDeltaTime * pixelHairSkull[p].a);
					}
			}
			
			HairSkullCombine = new Texture2D(atlasBase.width, atlasBase.height);
			HairSkullCombine.SetPixels(pixelAtlasBase);
			HairSkullCombine.SetPixels(384,0,640,384,pixelBaseHead);
			
			HairSkullCombine.Apply();
			HeadAddCombineF ();
		}
}

function HeadAddCombineF () {
		if(headAddN <= -1){headAddN = headAddQ - 1;}
		if(headAddN >= headAddQ){headAddN = 0;}
		if(headAddN >=0 && headAddN <= headAddQ - 1){	
			
			atlasBase = HairSkullCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(384,0,640,384);    	   // texture head base baseHead.GetPixels32();
			 var pixelHeadAdd = headAdd[headAddN].GetPixels32();   // Selected headAdd texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelHeadAdd[p].a != pixelBaseHead[p].a && pixelHeadAdd[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelHeadAdd[p],Time.fixedDeltaTime * pixelHeadAdd[p].a);
					}
			}
			
			
			HeadAddCombine = new Texture2D(atlasBase.width, atlasBase.height);
			HeadAddCombine.SetPixels(pixelAtlasBase);
			HeadAddCombine.SetPixels(384,0,640,384,pixelBaseHead);
			
			HeadAddCombine.Apply();
			EyeCombineF ();
		
		}
}
function EyeCombineF () {
		if(EyeN <= -1){EyeN = EyeQ - 1;}
		if(EyeN >= EyeQ){EyeN = 0;}
		if(EyeN >=0 && EyeN <= EyeQ - 1){	
			
			atlasBase = HeadAddCombine;

			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas		 
			 var pixelEye = Eye[EyeN].GetPixels();                           // Selected Eye texture 
		
			EyeCombine = new Texture2D(atlasBase.width, atlasBase.height);
			EyeCombine.SetPixels(pixelAtlasBase);
			EyeCombine.SetPixels(960,0,64,64,pixelEye);
			
			EyeCombine.Apply();
			PantCombineF ();
		}
}

function PantCombineF () {
		if(PantN <= -1){PantN = PantQ - 1;}
		if(PantN >= PantQ){PantN = 0;}
		if(PantN >=0 && PantN <= PantQ - 1){	
			
			atlasBase = EyeCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(0,0,512,576);    	   // texture head base baseHead.GetPixels32();
			 var pixelPant = Pant[PantN].GetPixels32();                           // Selected Pant texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelPant[p].a != pixelBaseHead[p].a && pixelPant[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelPant[p],Time.fixedDeltaTime * pixelPant[p].a);
					}
			}
			
			PantCombine = new Texture2D(atlasBase.width, atlasBase.height);
			PantCombine.SetPixels(pixelAtlasBase);
			PantCombine.SetPixels(0,0,512,576,pixelBaseHead);
			
			PantCombine.Apply();
			TorsoCombineF();
		}
}
function TorsoCombineF () {
		if(TorsoN <= -1){TorsoN = TorsoQ - 1;}
		if(TorsoN >= TorsoQ){TorsoN = 0;}
		if(TorsoN >=0 && TorsoN <= TorsoQ - 1){	
			
			atlasBase = PantCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(0,512,1024,512);    	   // texture head base baseHead.GetPixels32();
			 var pixelTorso = Torso[TorsoN].GetPixels32();                           // Selected Torso texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelTorso[p].a != pixelBaseHead[p].a && pixelTorso[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelTorso[p],Time.fixedDeltaTime * pixelTorso[p].a);
					}
			}
			
			TorsoCombine = new Texture2D(atlasBase.width, atlasBase.height);
			TorsoCombine.SetPixels(pixelAtlasBase);
			TorsoCombine.SetPixels(0,512,1024,512,pixelBaseHead);
			
			TorsoCombine.Apply();
			ShoeCombineF ();
		}
}
function ShoeCombineF () {
		if(ShoeN <= -1){ShoeN = ShoeQ - 1;}
		if(ShoeN >= ShoeQ){ShoeN = 0;}
		if(ShoeN >=0 && ShoeN <= ShoeQ - 1){	
			
			atlasBase = TorsoCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(0,0,384,448);    	   // texture head base baseHead.GetPixels32();
			 var pixelShoe = Shoe[ShoeN].GetPixels32();                           // Selected Shoe texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelShoe[p].a != pixelBaseHead[p].a && pixelShoe[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelShoe[p],Time.fixedDeltaTime * pixelShoe[p].a);
					}
			}

			ShoeCombine = new Texture2D(atlasBase.width, atlasBase.height);
			ShoeCombine.SetPixels(pixelAtlasBase);
			ShoeCombine.SetPixels(0,0,384,448,pixelBaseHead);
			ShoeCombine.Apply();
			GloveCombineF();
		}
}
function GloveCombineF () {
		if(GloveN <= -1){GloveN = GloveQ - 1;}
		if(GloveN >= GloveQ){GloveN = 0;}
		if(GloveN >=0 && GloveN <= GloveQ - 1){	
			
			atlasBase = ShoeCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(512,384,512,448);    	   // texture head base baseHead.GetPixels32();
			 var pixelGlove = Glove[GloveN].GetPixels32();                           // Selected Glove texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head 
				if(pixelGlove[p].a != pixelBaseHead[p].a && pixelGlove[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelGlove[p],Time.fixedDeltaTime * pixelGlove[p].a);
					}
			}

			GloveCombine = new Texture2D(atlasBase.width, atlasBase.height);
			GloveCombine.SetPixels(pixelAtlasBase);
			GloveCombine.SetPixels(512,384,512,448,pixelBaseHead);
			
			GloveCombine.Apply();
			RobeLongCombine ();
		}
}

function RobeLongCombine(){
		if(robeLongB == true){
			if(robeLongN <= -1){robeLongN = robeLongQ - 1;}
			if(robeLongN >= robeLongQ){robeLongN = 1;}
			if(robeLongN == 0){robeLongN++;}
			if(robeLongN >=1 && robeLongN <= robeLongQ - 1){	
				
				atlasBase = GloveCombine;
				 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
				 var pixelBaseHead =  atlasBase.GetPixels(0,0,512,576);    	   // texture head base baseHead.GetPixels32();
				 var pixelRobeLong = robeLongTex[robeLongN].GetPixels32();                           // Selected Pant texture 
				for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
					if(pixelRobeLong[p].a != pixelBaseHead[p].a && pixelRobeLong[p].a != 0){			          // apply color for transparent pixel 
						pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelRobeLong[p],Time.fixedDeltaTime * pixelRobeLong[p].a);
						}
				}
				
				robeLongCombine = new Texture2D(atlasBase.width, atlasBase.height);
				robeLongCombine.SetPixels(pixelAtlasBase);
				robeLongCombine.SetPixels(0,0,512,576,pixelBaseHead);
				
				robeLongCombine.Apply();
				RobeShortCombine();
			}

		}
		else {	
				atlasBase = GloveCombine;
				 var pixelAtlasBase1 = atlasBase.GetPixels();           // array of color pixel //full atlas
				 var pixelBaseHead1 =  atlasBase.GetPixels(0,0,512,576);    	   // texture head base baseHead.GetPixels32();
				 var pixelRobeLong1 = robeLongTex[0].GetPixels32();                           // Selected Pant texture 
				for(var p1 = 0; p1 < pixelBaseHead1.Length; ++p1){       //replace all pixel in head
					if(pixelRobeLong1[p1].a != pixelBaseHead1[p1].a && pixelRobeLong1[p1].a != 0){			          // apply color for transparent pixel 
						pixelBaseHead1[p1] = pixelBaseHead1[p1].Lerp(pixelBaseHead1[p1], pixelRobeLong1[p1],Time.fixedDeltaTime * pixelRobeLong1[p1].a);
						}
				}
				
				robeLongCombine = new Texture2D(atlasBase.width, atlasBase.height);
				robeLongCombine.SetPixels(pixelAtlasBase1);
				robeLongCombine.SetPixels(0,0,512,576,pixelBaseHead1);
				
				robeLongCombine.Apply();
				RobeShortCombine();
			}
		
}//
function RobeShortCombine(){
		if(robeShortB == true){
		if(robeShortN <= -1){robeShortN = robeShortQ - 1;}
			if(robeShortN >= robeShortQ){robeShortN = 1;}
			if(robeShortN == 0){robeShortN++;}
			if(robeShortN >=1 && robeShortN <= robeShortQ - 1){	
				
				atlasBase = robeLongCombine;
				 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
				 var pixelBaseHead =  atlasBase.GetPixels(0,0,512,576);    	   // texture head base baseHead.GetPixels32();
				 var pixelrobeShort = robeShortTex[robeShortN].GetPixels32();                           // Selected Pant texture 
				for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
					if(pixelrobeShort[p].a != pixelBaseHead[p].a && pixelrobeShort[p].a != 0){			          // apply color for transparent pixel 
						pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelrobeShort[p],Time.fixedDeltaTime * pixelrobeShort[p].a);
						}
				}
				
				robeShortCombine = new Texture2D(atlasBase.width, atlasBase.height);
				robeShortCombine.SetPixels(pixelAtlasBase);
				robeShortCombine.SetPixels(0,0,512,576,pixelBaseHead);
				
				robeShortCombine.Apply();
				BeltCombineF();
			
		 }
		
		
		}else {	
				atlasBase = robeLongCombine;
				 var pixelAtlasBase1 = atlasBase.GetPixels();           // array of color pixel //full atlas
				 var pixelBaseHead1 =  atlasBase.GetPixels(0,0,512,576);    	   // texture head base baseHead.GetPixels32();
				 var pixelrobeShort1 = robeShortTex[0].GetPixels32();                           // Selected Pant texture 
				for(var p1 = 0; p1 < pixelBaseHead1.Length; ++p1){       //replace all pixel in head
					if(pixelrobeShort1[p1].a != pixelBaseHead1[p1].a && pixelrobeShort1[p1].a != 0){			          // apply color for transparent pixel 
						pixelBaseHead1[p1] = pixelBaseHead1[p1].Lerp(pixelBaseHead1[p1], pixelrobeShort1[p1],Time.fixedDeltaTime * pixelrobeShort1[p1].a);
						}
				}
				
				robeShortCombine = new Texture2D(atlasBase.width, atlasBase.height); 
				robeShortCombine.SetPixels(pixelAtlasBase1);
				robeShortCombine.SetPixels(0,0,512,576,pixelBaseHead1);
				
				robeShortCombine.Apply();
				BeltCombineF();
			}
		

}

function BeltCombineF () {
		if(BeltN <= -1){BeltN = BeltQ - 1;}
		if(BeltN >= BeltQ){BeltN = 0;}
		if(BeltN >=0 && BeltN <= BeltQ - 1){	
			
			atlasBase = robeShortCombine;
			 var pixelAtlasBase = atlasBase.GetPixels();           // array of color pixel //full atlas
			 var pixelBaseHead =  atlasBase.GetPixels(0,512,512,256);    	   // texture head base baseHead.GetPixels32();
			 var pixelBelt = Belt[BeltN].GetPixels32();                           // Selected Belt texture 
			for(var p = 0; p < pixelBaseHead.Length; ++p){       //replace all pixel in head
				if(pixelBelt[p].a != pixelBaseHead[p].a && pixelBelt[p].a != 0){			          // apply color for transparent pixel 
					pixelBaseHead[p] = pixelBaseHead[p].Lerp(pixelBaseHead[p], pixelBelt[p],Time.fixedDeltaTime * pixelBelt[p].a);
					}
			}
			
			BeltCombine = new Texture2D(atlasBase.width, atlasBase.height);
			BeltCombine.SetPixels(pixelAtlasBase);
			BeltCombine.SetPixels(0,512,512,256,pixelBaseHead);
			
			BeltCombine.Apply();
			CombineAll = BeltCombine;
			for(var rendN = 0; rendN<ModelRenderers.length; rendN++){	
			ModelRenderers[rendN].material.mainTexture = BeltCombine;
			}
			planeRend.material.mainTexture = BeltCombine;		
			CombineAll = BeltCombine;
		}
}
////////////////////////////////////////////////////////////////////    ___    ____  __  _______  ____ 
////////////////////////////////////////////////////////////////////   /   |  / __ \/  |/  / __ \/ __ \////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////  / /| | / /_/ / /|_/ / / / / /_/ /////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////// / ___ |/ _, _/ /  / / /_/ / _, _/ ////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////_/  |_/_/ |_/_/  /_/\____/_/ |_| //////////////////////////////////////////////////////////////////// 
                                   
// all these function equip the selected item on the bone, based on the categorie
function HeadEquip(){
		// fix item number 
		if(headN < 0){headN = headQ -1;}
		if(headN > headQ -1){headN = 0;}
		// Equip head item base on is number
		if(headN >=0 && headN <= headQ ){
	 		headArmorS = headArmor[headN];
	 		equipedArmor[0] = headArmorS;
	 		headArmor[headN].transform.position = headAnchor.position;
	 		headArmor[headN].transform.rotation = headAnchor.rotation;
	 		headArmor[headN].transform.parent = headAnchor;
 		}
}
function hairModelEquip(){
		// fix item number 
		if(hairModelN < 0){hairModelN = hairModelQ -1;}
		if(hairModelN > hairModelQ -1){hairModelN = 0;}
		// Equip hairModel item base on is number
		
		//if(headN >= 1) {hairModelN = 0;}		          //remove hair if helm equiped

		if(hairModelN >=0 && hairModelN <= hairModelQ ){
	 		hairModelS = hairModel[hairModelN];
	 		equipedArmor[1] = hairModelS;
	 		hairModel[hairModelN].transform.position = headAnchor.position;
	 		hairModel[hairModelN].transform.rotation = headAnchor.rotation;
	 		hairModel[hairModelN].transform.parent = headAnchor;
 			//get the colorPilosity texture for hair model and match texture skin colorpilosity
			// 4 BECAUSE THERRE IS 4 COLOR FOR NOW !!!
 			hairTex = new Texture2D[4];                                                              								//resize array
			for(var ht = 0; ht < 4; ++ht){																      						   //fill with Texture2D
				hairTex[ht] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Armor/HairModel/"+hairModelN+"/A_HumanMale_HairModel_" +hairModelN+ "_Color_"+ht+".png", Texture2D) as Texture2D;
				}
   			hairTexQ = hairTex.Length;  
 			lodHairGet = hairModel[hairModelN].transform.GetComponentsInChildren.<Renderer>();
 			for(var LODhair = 0; LODhair < lodHairGet.length; LODhair++){	
				lodHairGet[LODhair].GetComponent.<Renderer>().material.mainTexture = hairTex[colorPilosityNumber];
				}
 			}
}
function JawEquip(){
		// fix item number 
		if(jawN < 0){jawN = jawQ - 1;}
		if(jawN > jawQ -1){jawN = 0;}
		// Equip jaw item base on is number
		if(jawN >=0 && jawN <= jawQ - 1){
	 		jawS = jaw[jawN];
	 		equipedArmor[2] = jawS;
	 		jaw[jawN].transform.position = jawAnchor.position;
	 		jaw[jawN].transform.rotation = jawAnchor.rotation;
	 		jaw[jawN].transform.parent = jawAnchor;
 		
 			//get the colorPilosity texture for hair model and match texture skin colorpilosity
			// 4 BECAUSE THERRE IS 4 COLOR FOR NOW !!!
 			jawTex = new Texture2D[4];                                                              								//resize array
			for(var jt = 0; jt < 4; ++jt){																      						   //fill with Texture2D
				jawTex[jt] = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/Textures/Armor/Jaw/"+jawN+"/A_HumanMale_BeardModel_" +jawN+ "_Color_"+jt+".png", Texture2D) as Texture2D;
				}
    		jawTexQ = jawTex.Length;  
 			 lodJawGet = jaw[jawN].transform.GetComponentsInChildren.<Renderer>();
 			for(var LODjaw = 0; LODjaw < lodJawGet.length; LODjaw++){	
				lodJawGet[LODjaw].GetComponent.<Renderer>().material.mainTexture = jawTex[colorPilosityNumber];
				} 		
 		}
}
function EyeEquip(){
		// fix item number 
		if(eyeN < 0){eyeN = eyeQ - 1;}
		if(eyeN > eyeQ -1){eyeN = 0;}
		// Equip eye item base on is number
		if(eyeN >=0 && eyeN <= eyeQ - 1){
	 		eyeS = eyeAddArmor[eyeN];
	 		eyeAddArmor[eyeN].transform.position = headAnchor.position;
	 		eyeAddArmor[eyeN].transform.rotation = headAnchor.rotation;
	 		eyeAddArmor[eyeN].transform.parent = headAnchor;
 		}
}
function TorsoEquip(){
		// fix item number 
		if(torsoN < 0){torsoN = torsoQ - 1;}
		if(torsoN > torsoQ - 1){torsoN = 0;}
		// Equip torso item base on is number
		if(torsoN >=0 && torsoN <= torsoQ - 1){
	 		torsoArmorS = torsoArmor[torsoN];
	 		equipedArmor[3] = torsoArmorS;
	 		torsoArmor[torsoN].transform.position = torsoAnchor.position;
	 		torsoArmor[torsoN].transform.rotation = torsoAnchor.rotation;
	 		torsoArmor[torsoN].transform.parent = torsoAnchor;
 		}
}
function TorsoAddEquip(){
		// fix item number 
		if(torsoAddN < 0){torsoAddN = torsoAddQ - 1;}
		if(torsoAddN > torsoAddQ -1){torsoAddN = 0;}
		// Equip torso item base on is number
		if(torsoAddN >=0 && torsoAddN <= torsoAddQ ){
	 		torsoAddArmorS = torsoAddArmor[torsoAddN];
	 		equipedArmor[4] = torsoAddArmorS;
	 		torsoAddArmor[torsoAddN].transform.position = torsoAnchor.position;
	 		torsoAddArmor[torsoAddN].transform.rotation = torsoAnchor.rotation;
	 		torsoAddArmor[torsoAddN].transform.parent = torsoAnchor;
 		}
}
function BeltEquip(){
		// fix item number 
		if(beltN < 0){beltN = beltQ - 1;}
		if(beltN > beltQ -1){beltN = 0;}
		// Equip belt item base on is number
		if(beltN >=0 && beltN <= beltQ ){
	 		beltArmorS = beltArmor[beltN];
	 		equipedArmor[5] = beltArmorS;
	 		beltArmor[beltN].transform.position = beltAnchor.position;
	 		beltArmor[beltN].transform.rotation = beltAnchor.rotation;
	 		beltArmor[beltN].transform.parent = beltAnchor;
 		}
}
function BeltAddEquip(){
		// fix item number 
		if(beltAddN < 0){beltAddN = beltAddQ - 1;}
		if(beltAddN > beltAddQ-1){beltAddN = 0;}
		// Equip belt item base on is number
		if(beltAddN >=0 && beltAddN <= beltAddQ - 1){
	 		beltAddArmorS = beltAddArmor[beltAddN];
	 		equipedArmor[6] = beltAddArmorS;
	 		beltAddArmor[beltAddN].transform.position = beltAnchor.position;
	 		beltAddArmor[beltAddN].transform.rotation = beltAnchor.rotation;
	 		beltAddArmor[beltAddN].transform.parent = beltAnchor;
 		}
}
function ShoulderREquip(){
		// fix item number 
		if(shoulderRN < 0){shoulderRN = shoulderRQ - 1;}
		if(shoulderRN > shoulderRQ -1){shoulderRN = 0;}
		// Equip shoulderR item base on is number
		if(shoulderRN >=0 && shoulderRN <= shoulderRQ -1 ){
	 		shoulderRArmorS = shoulderRArmor[shoulderRN];
	 		equipedArmor[7] = shoulderRArmorS;
	 		shoulderRArmor[shoulderRN].transform.position = shoulderRAnchor.position;
	 		shoulderRArmor[shoulderRN].transform.rotation = shoulderRAnchor.rotation;
	 		shoulderRArmor[shoulderRN].transform.parent = shoulderRAnchor;
 		}
}
function ShoulderLEquip(){
		// fix item number 
		if(shoulderLN < 0){shoulderLN = shoulderLQ - 1;}
		if(shoulderLN > shoulderLQ -1){shoulderLN = 0;}
		// Equip shoulderL item base on is number
		if(shoulderLN >=0 && shoulderLN <= shoulderLQ - 1){
	 		shoulderLArmorS = shoulderLArmor[shoulderLN];
	 		equipedArmor[8] = shoulderLArmorS;
	 		shoulderLArmor[shoulderLN].transform.position = shoulderLAnchor.position;
	 		shoulderLArmor[shoulderLN].transform.rotation = shoulderLAnchor.rotation;
	 		shoulderLArmor[shoulderLN].transform.parent = shoulderLAnchor;
 		}
}
function ArmREquip(){
		// fix item number 
		if(armRN < 0){armRN = armRQ - 1;}
		if(armRN > armRQ -1){armRN = 0;}
		// Equip armR item base on is number
		if(armRN >=0 && armRN <= armRQ - 1){
	 		armRArmorS = armRArmor[armRN];
	 		equipedArmor[9] = armRArmorS;
	 		armRArmor[armRN].transform.position = armRAnchor.position;
	 		armRArmor[armRN].transform.rotation = armRAnchor.rotation;
	 		armRArmor[armRN].transform.parent = armRAnchor;
 		}
}
function ArmLEquip(){
		// fix item number 
		if(armLN < 0){armLN = armLQ - 1;}
		if(armLN > armLQ -1){armLN = 0;}
		// Equip armL item base on is number
		if(armLN >=0 && armLN <= armLQ - 1){
	 		armLArmorS = armLArmor[armLN];
	 		equipedArmor[10] = armLArmorS;
	 		armLArmor[armLN].transform.position = armLAnchor.position;
	 		armLArmor[armLN].transform.rotation = armLAnchor.rotation;
	 		armLArmor[armLN].transform.parent = armLAnchor;
 		}
}
function LegREquip(){
		// fix item number 
		if(legRN < 0){legRN = legRQ - 1;}
		if(legRN > legRQ -1){legRN = 0;}
		// Equip legR item base on is number
		if(legRN >=0 && legRN <= legRQ - 1){
	 		legRArmorS = legRArmor[legRN];
	 		equipedArmor[11] = legRArmorS;
	 		legRArmor[legRN].transform.position = legRAnchor.position;
	 		legRArmor[legRN].transform.rotation = legRAnchor.rotation;
	 		legRArmor[legRN].transform.parent = legRAnchor;
 		}
}
function LegLEquip(){
		// fix item number 
		if(legLN < 0){legLN = legLQ - 1;}
		if(legLN > legLQ -1){legLN = 0;}
		// Equip legL item base on is number
		if(legLN >=0 && legLN <= legLQ - 1){
	 		legLArmorS = legLArmor[legLN];
	 		equipedArmor[12] = legLArmorS;
	 		legLArmor[legLN].transform.position = legLAnchor.position;
	 		legLArmor[legLN].transform.rotation = legLAnchor.rotation;
	 		legLArmor[legLN].transform.parent = legLAnchor;
 		}
}
function WeaponREquip(){
		// fix item number 
		if(weaponRN <= 0){weaponRN = weaponRQ - 1;}
		if(weaponRN >= weaponRQ){weaponRN = 1;}
		// Equip weaponR item base on is number
		if(weaponRN >=0 && weaponRN <= weaponRQ - 1){
	 		weaponRArmorS = weaponRArmor[weaponRN];
	 		equipedArmor[13] = weaponRArmorS;
	 		weaponRArmor[weaponRN].transform.position = weaponRAnchor.position;
	 		weaponRArmor[weaponRN].transform.rotation = weaponRAnchor.rotation;
	 		weaponRArmor[weaponRN].transform.parent = weaponRAnchor;
		//weaponRArmorSTexture = weaponRArmor[weaponRN].GetComponent.<Renderer>().material.mainTexture as Texture2D;
 		
 		
 		}
}
function WeaponLEquip(){
		// fix item number 
		if(weaponLN <= 0){weaponLN = weaponLQ - 1;}
		if(weaponLN >= weaponLQ){weaponLN = 1;}
		// Equip weaponL item base on is number
		if(weaponLN >=0 && weaponLN <= weaponLQ - 1){
	 		weaponLArmorS = weaponLArmor[weaponLN];
	 		equipedArmor[14] = weaponLArmorS;
	 		weaponLArmor[weaponLN].transform.position = weaponLAnchor.position;
	 		weaponLArmor[weaponLN].transform.rotation = weaponLAnchor.rotation;
	 		weaponLArmor[weaponLN].transform.parent = weaponLAnchor;
 		}
}
function FXEquip(){
		// fix item number 
		if(FXN <= 0){FXN = FXQ - 1;}
		if(FXN >= FXQ){FXN = 1;}
		// Equip FX item base on is number
		if(FXN >=0 && FXN <= FXQ - 1){
	 		FXArmorS = FXArmor[FXN];
	 		FXArmor[FXN].transform.position = FXAnchor.position;
	 		FXArmor[FXN].transform.rotation = FXAnchor.rotation;
	 		FXArmor[FXN].transform.parent = FXAnchor;
 		}
}


function CloakOn(){
	if(cloakN <= 0){cloakN = cloakQ - 1;}
	if(cloakN >= cloakQ){cloakN = 0;}
	if(cloakN >=0 && cloakN <= cloakQ - 1){
		for(var clo = 0;clo < cloak.Length; clo++ ){
		cloak[clo].SetActive(true);
		cloak[clo].transform.GetComponent.<Renderer>().material.mainTexture = cloakTex[cloakN];
		}
	}

}
function CloakOff(){
	for(var clo1 = 0;clo1 < cloak.Length; clo1++ ){
	cloak[clo1].SetActive(false);
	}

}

function RobeLongOn(){
	if(robeLongN <= 0){robeLongN = robeLongQ - 1;}
	if(robeLongN >= robeLongQ){robeLongN = 0;}
	if(robeLongN >=0 && robeLongN <= robeLongQ - 1){
		for(var rl1 = 0;rl1 < LongRobe.Length; rl1++ ){
		LongRobe[rl1].SetActive(true);
		}
	}

}
function RobeLongOff(){
	for(var rl2 = 0;rl2 < LongRobe.Length; rl2++ ){
	LongRobe[rl2].SetActive(false);
	}

}

function RobeShortOn(){
	if(robeShortN <= 0){robeShortN = robeShortQ - 1;}
	if(robeShortN >= robeShortQ){robeShortN = 0;}
	if(robeShortN >=0 && robeShortN <= robeShortQ - 1){
		for(var rl1 = 0;rl1 < ShortRobe.Length; rl1++ ){
		ShortRobe[rl1].SetActive(true);
		}
	}

}
function RobeShortOff(){
	for(var rl2 = 0;rl2 < ShortRobe.Length; rl2++ ){
	ShortRobe[rl2].SetActive(false);
	}

}






//Armor texture render

//Update FeedBack Texture Armor
// this function give a feedback on the armor texture to be exported on the save function
function FBTexArmor (){
	AllArmorsPartQ=0;
	for(var fbtxa = 0; fbtxa < ArmorpartEquip.Length; fbtxa++){
		if(ArmorpartEquip[fbtxa] == true){
		AllArmorsPartQ++;
		}
	}
	
	if(AllArmorsPartQ == 0 ){
	planeArmor512.SetActive(false);
	planeArmor1024.SetActive(false);
	planeArmor2048.SetActive(false);
	}
	
	if(AllArmorsPartQ == 1 ){
		planeArmor512.SetActive(true);
		planeArmor1024.SetActive(false);
		planeArmor2048.SetActive(false);
		for(var texA1 = 0; texA1 < equipedArmor.length; texA1++){
			if(equipedArmor[texA1] != null){
				MatArmor = equipedArmor[texA1].GetComponentInChildren.<Renderer>().material;
				planeArmor512.transform.GetComponent.<Renderer>().material = MatArmor;
				}
			}
	}
	
	if(AllArmorsPartQ == 2 || AllArmorsPartQ == 3 || AllArmorsPartQ == 4 ){
		MatItemNumber = 0;
		planeArmor512.SetActive(false);
		planeArmor1024.SetActive(true);
		planeArmor2048.SetActive(false);
		//reset all texture mat
		//for(var rez1 = 0; rez1 <= AllArmorsPartQ; rez1++){
			
	//	}
		for(var texA2 = 0; texA2 < equipedArmor.length; texA2++){
			if(equipedArmor[texA2] != null){
				MatItemNumber++;
				
				for(var itn =0; itn<=MatItemNumber; itn++){
				MatArmorPart[itn] = equipedArmor[texA2].GetComponentInChildren.<Renderer>().material;
				if(MatItemNumber == 1){
					planeArmor1024Tex[0].transform.GetComponent.<Renderer>().material = MatArmorPart[itn];
					planeArmor1024Tex[1].transform.GetComponent.<Renderer>().material = NoneMat;
					planeArmor1024Tex[2].transform.GetComponent.<Renderer>().material = NoneMat;
					planeArmor1024Tex[3].transform.GetComponent.<Renderer>().material = NoneMat;
				}
				if(MatItemNumber == 2){
					planeArmor1024Tex[1].transform.GetComponent.<Renderer>().material = MatArmorPart[itn];
					planeArmor1024Tex[2].transform.GetComponent.<Renderer>().material = NoneMat;		
					planeArmor1024Tex[3].transform.GetComponent.<Renderer>().material = NoneMat;
				}
				if(MatItemNumber == 3){
					planeArmor1024Tex[2].transform.GetComponent.<Renderer>().material = MatArmorPart[itn];
					planeArmor1024Tex[3].transform.GetComponent.<Renderer>().material = NoneMat;
				}
				if(MatItemNumber == 4){
					planeArmor1024Tex[3].transform.GetComponent.<Renderer>().material = MatArmorPart[itn];
				}
				}
			}
		}
	
	}
	
	if(AllArmorsPartQ > 4 ){
		MatItemNumber = 0;
		planeArmor512.SetActive(false);
		planeArmor1024.SetActive(false);
		planeArmor2048.SetActive(true);
		for(var texA3 = 0; texA3 < equipedArmor.length; texA3++){
			if(equipedArmor[texA3] != null){
				MatItemNumber++;
				for(var itn1 =0; itn1<=MatItemNumber; itn1++){
				MatArmorPart[itn1] = equipedArmor[texA3].GetComponentInChildren.<Renderer>().material;
					if(MatItemNumber == 1){
						planeArmor2048Tex[0].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
					} 
					if(MatItemNumber == 2){
						planeArmor2048Tex[1].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[2].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[3].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[4].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[5].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					} 
					if(MatItemNumber == 3){
						planeArmor2048Tex[2].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[3].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[4].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[5].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					} 
					if(MatItemNumber == 4){
						planeArmor2048Tex[3].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[4].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[5].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 5){
						planeArmor2048Tex[4].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[5].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 6){
						planeArmor2048Tex[5].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 7){
						planeArmor2048Tex[6].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 8){
						planeArmor2048Tex[7].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 9){
						planeArmor2048Tex[8].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 10){
						planeArmor2048Tex[9].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 11){
						planeArmor2048Tex[10].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 12){
						planeArmor2048Tex[11].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 13){
						planeArmor2048Tex[12].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 14){
						planeArmor2048Tex[13].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = NoneMat;
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 15){
						planeArmor2048Tex[14].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = NoneMat;
					}
					if(MatItemNumber == 16){
						planeArmor2048Tex[15].transform.GetComponent.<Renderer>().material = MatArmorPart[itn1];
					}
					
					
				}
			}
		}
	}
}
 

// SAVE FUNCTION 

function MakePrefab (){
		
		//create a new folder to receive the New hierachy folder for the new character under new character folder 
	for(var foldHuman =0; foldHuman<50; foldHuman++){
		if(AssetDatabase.IsValidFolder("Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"")== true){SavedPrefabNum ++;}
		}
		
	var folder = AssetDatabase.CreateFolder("Assets/Character_Editor/NewCharacter", "HumanMale"+ SavedPrefabNum +"");
	
//if pack armor button ON
	var folder1 = AssetDatabase.CreateFolder("Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"", "Mesh");
	// create material for armor in folder
	var materialArmor = new Material (Shader.Find("Unlit/Texture"));
	AssetDatabase.CreateAsset(materialArmor, "Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/HumanMale"+ SavedPrefabNum +"ArmorMat.mat");
//
	
	
	AssetDatabase.Refresh();
	// get the quantity of equiped armor part by boolean true or false = AllArmorsPartQ
	for(var y = 0; y < ArmorpartEquip.Length; y++){
		if(ArmorpartEquip[y] == true){
		AllArmorsPartQ++;
		}
	}

	
	// if only 1 armor part are equiped on the character // no need to switch and replace uw but mesh and texture will be exported 
		if(AllArmorsPartQ == 1 ){
			TextureArmor = new Texture2D(512, 512); 
			for(var truc1 = 0; truc1 < equipedArmor.length; truc1++){
				if(equipedArmor[truc1] != null){
					TextureArmor = equipedArmor[truc1].GetComponentInChildren.<Renderer>().material.mainTexture as Texture2D;
					armorsParts = new MeshFilter[3];	                                                                           // set the array length for LOD inside the item
					armorsParts = equipedArmor[truc1].GetComponentsInChildren.<MeshFilter>();                                       //get the meshfilter of LOD and store in array
						for(var  LODAr1 = 0; LODAr1<armorsParts.Length; LODAr1++){		                                             //check for LOD inside the item the array is determined with 3 level 						
							if(armorsParts[LODAr1] != null){ 
								armorsParts[LODAr1].GetComponent.<Renderer>().material = materialArmor;
								var m1 : Mesh = new Mesh ();																	   //create new mesh to save in new created folder
								m1 = armorsParts[LODAr1].mesh;																   //assigne the selected LOD Mesh with new UV's to the new mesh to be exported
								AssetDatabase.CreateAsset(m1, "Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/Mesh/"+armorsParts[LODAr1].name+"_New"+LODAr1+SavedPrefabNum+".asset");//exporte asset to new project folder	
							}
						}
				 }
			}
		}
	
	
	// if only 2 armor part are equiped on the character 	
	if(AllArmorsPartQ == 2 || AllArmorsPartQ == 3 || AllArmorsPartQ == 4){		
		TextureArmor = new Texture2D(1024, 1024); 																				//resize the texture for armor that will be exported in this case of only 2 equiped armor part 1024/1024
		for(var Ar2 = 0; Ar2 < equipedArmor.length; Ar2++){                                                                     // look for equiped armor part mesh filter for resizing UW
			if(equipedArmor[Ar2] != null){																						
				itemNumber++;																									// increment the item (armor part selection)
				TextureArmorPart[Ar2] = equipedArmor[Ar2].GetComponentInChildren.<Renderer>().material.mainTexture as Texture2D;// get the texture of this selected armor part
				armorsParts = new MeshFilter[3];	                                                                           // set the array length for LOD inside the item
				armorsParts = equipedArmor[Ar2].GetComponentsInChildren.<MeshFilter>();                                       //get the meshfilter of LOD and store in array
				
				for(var  LODAr2 = 0; LODAr2<armorsParts.Length; LODAr2++){		                                             //check for LOD inside the item the array is determined with 3 level 						
					if(armorsParts[LODAr2] != null){ 									                                    // if there is a LOD inside item
						armorsParts[LODAr2].GetComponent.<Renderer>().material = materialArmor;	                                           //assigne the new created material in new folder to the LOD to be exported
						var uvs : Vector2[] = armorsParts[LODAr2].GetComponent.<MeshFilter>().mesh.uv;                    //get the uw of the selected LOD
						
						for (var i: int = 0; i < uvs.Length; i++ ) {														
							if(itemNumber == 1){
								uvs[i] = Vector2 (uvs[i].x /2  , uvs[i].y /2 + 0.5);                                        // Offset all the UV's for a first item
							}
							if(itemNumber == 2){
								uvs[i] = Vector2 (uvs[i].x /2 + 0.5  , uvs[i].y /2 + 0.5);                               // Offset all the UV's for a second item
							}
							if(itemNumber == 3){
								uvs[i] = Vector2 (uvs[i].x /2   , uvs[i].y /2 );                               // Offset all the UV's for a second item
							}
							if(itemNumber == 4){
								uvs[i] = Vector2 (uvs[i].x /2 + 0.5 , uvs[i].y /2 );                               // Offset all the UV's for a second item
							}						
						}
					armorsParts[LODAr2].mesh.uv = uvs;                                                             //assigne new UV's to the selected LOD
					var m2 : Mesh = new Mesh ();																	   //create new mesh to save in new created folder
					m2 = armorsParts[LODAr2].mesh;																   //assigne the selected LOD Mesh with new UV's to the new mesh to be exported
					AssetDatabase.CreateAsset(m2, "Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/Mesh/"+armorsParts[LODAr2].name+"_New"+LODAr2+SavedPrefabNum+".asset");//exporte asset to new project folder
					
					}
				}	
				if(itemNumber == 1){																			//set the texture for the first item a the rigth place on a new texture to be exported
				var pix = TextureArmorPart[Ar2].GetPixels32();
				TextureArmor.SetPixels32(0,512,512,512,pix);			
				TextureArmor.Apply();
				}
				if(itemNumber == 2){																			//set the texture for the first item a the rigth place on a new texture to be exported
				//set the texture a the rigth place
				var pix2 = TextureArmorPart[Ar2].GetPixels32();
				TextureArmor.SetPixels32(512,512,512,512,pix2);	
				TextureArmor.Apply();		
				}
				if(itemNumber == 3){																			//set the texture for the first item a the rigth place on a new texture to be exported
				//set the texture a the rigth place
				var pix3 = TextureArmorPart[Ar2].GetPixels32();
				TextureArmor.SetPixels32(0,0,512,512,pix3);	
				TextureArmor.Apply();		
				}
				if(itemNumber == 4){																			//set the texture for the first item a the rigth place on a new texture to be exported
				//set the texture a the rigth place
				var pix4 = TextureArmorPart[Ar2].GetPixels32();
				TextureArmor.SetPixels32(512,0,512,512,pix4);	
				TextureArmor.Apply();		
				}				
			}
		}
	}
 // if the number of item equiped on the character is more than 4 and up to 16
		if(AllArmorsPartQ > 4 ){
		TextureArmor = new Texture2D(2048, 2048);
		for(var Ar3 = 0; Ar3 < equipedArmor.length; Ar3++){                                                                     // look for equiped armor part mesh filter for resizing UW
			if(equipedArmor[Ar3] != null){																						
				itemNumber++;																									// increment the item (armor part selection)
				TextureArmorPart[Ar3] = equipedArmor[Ar3].GetComponentInChildren.<Renderer>().material.mainTexture as Texture2D;// get the texture of this selected armor part
				armorsParts = new MeshFilter[3];	                                                                           // set the array length for LOD inside the item
				armorsParts = equipedArmor[Ar3].GetComponentsInChildren.<MeshFilter>();                                       //get the meshfilter of LOD and store in array
				
				for(var  LODAr3 = 0; LODAr3<armorsParts.Length; LODAr3++){		                                             //check for LOD inside the item the array is determined with 3 level 						
					if(armorsParts[LODAr3] != null){ 									                                    // if there is a LOD inside item
						armorsParts[LODAr3].GetComponent.<Renderer>().material = materialArmor;	                                           //assigne the new created material in new folder to the LOD to be exported
						var uvs1 : Vector2[] = armorsParts[LODAr3].GetComponent.<MeshFilter>().mesh.uv;                    //get the uw of the selected LOD
						
						for (var uvi: int = 0; uvi < uvs1.Length; uvi++ ) {														
							if(itemNumber == 1){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4  , uvs1 [uvi].y /4 + 0.75);                                        // Offset all the UV's for a first item
								}
							if(itemNumber == 2){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.25  , uvs1 [uvi].y /4 + 0.75);                               // Offset all the UV's for a second item
								}
							if(itemNumber == 3){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4   , uvs1 [uvi].y /4 +0.5);                               // Offset all the UV's for a second item
								}
							if(itemNumber == 4){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.25 , uvs1 [uvi].y /4+0.5 );                               // Offset all the UV's for a second item
								}	
							if(itemNumber == 5){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.5 , uvs1 [uvi].y /4+0.75 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 6){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.75 , uvs1 [uvi].y /4+0.75 );                               // Offset all the UV's for a second item
								}								
							if(itemNumber == 7){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.5 , uvs1 [uvi].y /4+0.5 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 8){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 + 0.75 , uvs1 [uvi].y /4+0.5 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 9){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 , uvs1 [uvi].y /4+0.25 );                               // Offset all the UV's for a second item
								}	
							if(itemNumber == 10){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.25 , uvs1 [uvi].y /4+0.25 );                               // Offset all the UV's for a second item
								}	
							if(itemNumber == 11){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.5 , uvs1 [uvi].y /4+0.25 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 12){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.75 , uvs1 [uvi].y /4+0.25 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 13){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4 , uvs1 [uvi].y /4 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 14){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.25 , uvs1 [uvi].y /4 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 15){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.5 , uvs1 [uvi].y /4 );                               // Offset all the UV's for a second item
								}
							if(itemNumber == 16){
								uvs1 [uvi] = Vector2 (uvs1 [uvi].x /4+0.75 , uvs1 [uvi].y /4 );                               // Offset all the UV's for a second item
								}					
							}
							
					armorsParts[LODAr3].mesh.uv = uvs1;                                                             //assigne new UV's to the selected LOD
					var m3 : Mesh = new Mesh ();																	   //create new mesh to save in new created folder
					m3 = armorsParts[LODAr3].mesh;																   //assigne the selected LOD Mesh with new UV's to the new mesh to be exported
					AssetDatabase.CreateAsset(m3, "Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/Mesh/"+armorsParts[LODAr3].name+"_New"+LODAr3+SavedPrefabNum+".asset");//exporte asset to new project folder
					}
					}
					if(itemNumber == 1){																			//set the texture for the first item a the rigth place on a new texture to be exported
					var pixel = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(0,1536,512,512,pixel);			
					TextureArmor.Apply();
					}
					if(itemNumber == 2){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel2 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(512,1536,512,512,pixel2);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 3){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel3 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(0,1024,512,512,pixel3);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 4){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel4 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(512,1024,512,512,pixel4);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 5){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel5 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1024,1536,512,512,pixel5);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 6){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel6 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1536,1536,512,512,pixel6);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 7){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel7 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1024,1024,512,512,pixel7);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 8){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel8 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1536,1024,512,512,pixel8);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 9){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel9 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(0,512,512,512,pixel9);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 10){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel10 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(512,512,512,512,pixel10);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 11){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel11 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1024,512,512,512,pixel11);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 12){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel12 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1536,512,512,512,pixel12);	
					TextureArmor.Apply();		
					}
					if(itemNumber == 13){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel13 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(0,0,512,512,pixel13);	
					TextureArmor.Apply();				
					}
					if(itemNumber == 14){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel14 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(512,0,512,512,pixel14);	
					TextureArmor.Apply();				
					}
					if(itemNumber == 15){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel15 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1024,0,512,512,pixel15);	
					TextureArmor.Apply();				
					}
					if(itemNumber == 16){																			//set the texture for the first item a the rigth place on a new texture to be exported
					//set the texture a the rigth place
					var pixel16 = TextureArmorPart[Ar3].GetPixels32();
					TextureArmor.SetPixels32(1536,0,512,512,pixel16);	
					TextureArmor.Apply();				
					}
				}
	}
	}
	
		//save the armor texture
	//create a new materials inside new character folder and assign it to the scene character to save the prefab

	var bytes1 = TextureArmor.EncodeToPNG();
	var path1 = EditorUtility.SaveFilePanel("Save Texture", "char","HumanMale"+ SavedPrefabNum +"Armor.png", "png");
	File.WriteAllBytes(path1,bytes1);
	AssetDatabase.Refresh();	
	materialArmor.mainTexture = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/HumanMale"+ SavedPrefabNum +"Armor.png", Texture2D) as Texture2D;
	
	
	
	
	//save the character skin Texture
	var bytes = CombineAll.EncodeToPNG();
	var path = EditorUtility.SaveFilePanel("Save Texture", "char","HumanMale"+ SavedPrefabNum +"CharacterSkin.png", "png");
	File.WriteAllBytes(path,bytes);
	AssetDatabase.Refresh();
	var materialSkin = new Material (Shader.Find("Unlit/Texture"));
	AssetDatabase.CreateAsset(materialSkin, "Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/HumanMale"+ SavedPrefabNum +"SkinMat.mat");	
	//assigne the texture to the new material
	materialSkin.mainTexture = AssetDatabase.LoadAssetAtPath("Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/HumanMale"+ SavedPrefabNum +"CharacterSkin.png", Texture2D) as Texture2D;
	//assigne the new material to the character and LOD
	for(var rendN = 0; rendN<ModelRenderers.length; rendN++){	
		ModelRenderers[rendN].material = materialSkin;
		}
	

	//Save the prefab				 			  				  				  				 
 	var prefab : Object = EditorUtility.CreateEmptyPrefab ("Assets/Character_Editor/NewCharacter/HumanMale"+ SavedPrefabNum +"/HumanMale" + SavedPrefabNum + ".prefab");
	PrefabUtility.ReplacePrefab(Character, prefab, ReplacePrefabOptions.ConnectToPrefab);
								
	// quit the editor because the mesh you exporte have now new UW'S and quitting the editor play mode will remove the new UW's 
	EditorApplication.isPlaying = false;
}

 



		 		
		 		
		 		
		 		
		 		
		 		
		 		
		 		
		 		
		 		 		