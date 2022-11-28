using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyAdjust : MonoBehaviour{

    Color thisColor;
    //Renderer rend;
    //public Material material1;
    Material material2;
    //float duration = 2.0f;

    private Coroutine run;
    private bool running = false;

    public float transparency = 0.0f;
    public float dissolveHeight = 0.0f;

    public float objectHeight = 5.0f;

    //public float load = 0.0f;

    // Start is called before the first frame update
    void Start(){
        thisColor = GetComponent<MeshRenderer>().material.color;

        material2 = GetComponent<MeshRenderer>().materials[1];
    }

    // Update is called once per frame
    void Update(){
        thisColor = new Color(thisColor.r, thisColor.g, thisColor.b, Mathf.Clamp(transparency, 0.0f, 1.0f));
        thisColor.a = Mathf.Clamp(transparency, 0.0f, 1.0f);

        this.GetComponent<MeshRenderer>().material.color = thisColor;

        material2.SetFloat("_CutoffHeight", Mathf.Clamp(dissolveHeight, -1.0f, objectHeight));

        if (Input.GetKeyDown("v")) {
            
        }
        
        if (Input.GetKeyDown("b")) {
            run = StartCoroutine(UnloadEffect());
        }
    }


    public void LoadEffectCall() {
        if(!running)
            run = StartCoroutine(LoadEffect());
    }


    public void UnloadEffectCall() {
        if (!running)
            run = StartCoroutine(UnloadEffect());
    }


    private IEnumerator LoadEffect() {

        print("dissolve effect A");
        while (true) {

            

            if (dissolveHeight < objectHeight)
                dissolveHeight += Time.deltaTime/2;

            if (dissolveHeight >= objectHeight)
                transparency += Time.deltaTime/2;

            if (transparency >= 1.0f) {
                if(run != null)
                    StopCoroutine(run);
                running = false;
            } else
                running = true;

            yield return null;
        } 
    }

    private IEnumerator UnloadEffect() {

        print("dissolve effect B");
        while (true) {

            if (transparency > 0.0f)
                transparency -= Time.deltaTime*2;

            if (transparency <= 0.0f)
                dissolveHeight -= Time.deltaTime*2;

            if (dissolveHeight <= -1.0f) {
                if(run!=null)
                    StopCoroutine(run);
                running = false;
            } else 
                running = true;
            

            yield return null;
        }
    }

}
