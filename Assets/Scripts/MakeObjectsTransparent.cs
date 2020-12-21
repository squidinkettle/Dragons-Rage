using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeObjectsTransparent : MonoBehaviour
{
    public GameObject player;
    RaycastHit hit;
    public float distance = 15f;
    RaycastHit[] hits;
    Renderer renderer;
    float fadeAmount;
    Material material;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(Fade());
    }

    IEnumerator Fade() {

        while (true)
        {
            Debug.Log("Inside Fade");
            fadeAmount = 0.1f;
            //var material = GetComponent<Renderer>().material;
            if (hit.collider!=null)
            {
                var color = material.color;

                color.a = fadeAmount;

                while (fadeAmount <= 0.9f)
                {
                    Debug.Log("FadingMaterial");
                    Debug.Log(fadeAmount);
                    fadeAmount += (0.11f * Time.deltaTime);
                    hit.collider.GetComponent<Renderer>().material.color = new Color(color.r, color.g, color.b, fadeAmount + (0.11f * Time.deltaTime));
                    yield return null;
                }
            }
            Debug.Log("Inside Fade");
             if (renderer != null)
             {
                 for (float f = 1f; f >= 0; f -= 0.1f)
                 {
                     Debug.Log("fading");
                     Color c = renderer.material.color;
                     c.a = f;
                     renderer.material.color = c;
                     yield return null;
                 }
             }
            yield return new WaitForSeconds(0.5f);
        }
          }
    // Update is called once per frame
    void Update()
    {
            var distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);
            hits = Physics.RaycastAll(transform.position, transform.forward, distanceToPlayer-20f);
            foreach(RaycastHit _hit in hits) {
               
                hit = _hit;

            if (hit.collider.GetComponent<House>())
            {

                renderer = _hit.collider.GetComponent<Renderer>();
        
                foreach (var mat in renderer.materials)
                {
                    SetAlpha(mat, 0.5f);

                }
                hit.collider.GetComponent<House>().SetIsTransparent(true);

            }




        }
        }


    void SetAlpha(Material mat, float alphaVal) {

        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);
        mat.SetColor("_Color", newColor);
    
    
    }
}
