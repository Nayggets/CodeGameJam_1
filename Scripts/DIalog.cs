using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DIalog : MonoBehaviour
{
    public Transform pnj;
    public LayerMask layermask;
	public GameObject textToActive;
    public GameObject PnjImage;
    public GameObject PlayerImage;

    private bool AlreadyActive = false;

    private bool Toaffiche = false;
    
    private bool TextLaunch = false;
    public Vector2 Range;
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    // Start is called before the first frame update

    IEnumerator Type()
    {
            foreach(char letter in sentences[index].ToCharArray()){
                if(Physics2D.OverlapBox(pnj.position, Range, 1, layermask) == false){
                    break;
                }

                textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
    }
    

    private void Start() 
    {
    }
    // Update is called once per frame
    void Update()
    {
        if(Physics2D.OverlapBox(pnj.position, Range, 1, layermask) == true){
            if(AlreadyActive == false){
                textToActive.SetActive(true);
                AlreadyActive = true;
            }
            if(textDisplay.text == sentences[index]){ 
                if(Input.GetKeyDown(KeyCode.E)){
                    NextSentence();
                    Toaffiche = !Toaffiche;
                    if(Toaffiche == false){
                        PnjImage.SetActive(false);
                        PlayerImage.SetActive(true);
                    }
                    else if(Toaffiche == true){
                        PlayerImage.SetActive(false);
                        if(index != sentences.Length -1){
                            PnjImage.SetActive(true);
                        }
                        else{
                            textToActive.SetActive(false);
                        }
                    }
                }
            }
            else if(Input.GetKeyDown(KeyCode.E) && TextLaunch == false){
                textToActive.SetActive(true);
                StartCoroutine(Type());
                TextLaunch = true;
                PnjImage.SetActive(false);
                PlayerImage.SetActive(true);
                Toaffiche = false;
            }   
            if(index >= sentences.Length){
                textToActive.SetActive(false);
                PlayerImage.SetActive(false);
                PnjImage.SetActive(false);
            }
            
        }
        else{
            textToActive.SetActive(false);
            if(TextLaunch == true){
                index = 0;
                textDisplay.text = "";
                TextLaunch = false;
            }
            PlayerImage.SetActive(false);
            PnjImage.SetActive(false);
            AlreadyActive = false;
        }
    }
    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else{
            textDisplay.text = "";
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pnj.position, Range);
    }
}
