using UnityEngine;
using System.Collections;

public class Test_ListItem : MonoBehaviour {

	Color icon_colorStart;
	Color description_colorStart;
	Transform icontf;
	Transform texttf;
	Transform linetf;
	Transform imagetf;
	Transform pricetf;
	float duration1 = 0.1f;
	float duration2 = 0.25f;
	float t =0f;
	public bool fadeout = false;
	public bool fadein = false;
	public bool fadeoutPriceButton = false;
	public bool fadeinPriceButton = false;
	
	void Start () {
		icontf = transform.Find ("Icon");
		linetf = transform.Find ("Line");
		texttf = transform.Find ("Text");
		imagetf = transform.Find ("Image");
		pricetf = transform.Find ("Price");
		//icon_colorStart = icontf.gameObject.GetComponent<Renderer> ().material.color;
		//icontf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,icon_colorStart,0);

		//description_colorStart = destf.gameObject.GetComponent<Renderer> ().material.color;
		//destf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,description_colorStart,0);

	}
	
	void Update() {
		if(fadeout==true)
			FadeOut();
		if(fadein==true)
			FadeIn();
		if (fadeoutPriceButton) {
			pricetf.GetComponent<PriceButton>().fadeout=true;
			fadeoutPriceButton=false;
		}
		if (fadeinPriceButton) {
			pricetf.GetComponent<PriceButton>().fadein=true;
			fadeinPriceButton=false;
		}
	}
	
	void FadeOut ()
	{

			if (t < duration1) {
				t += Time.deltaTime;
			//icontf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (icon_colorStart, Color.clear, t / duration1);
			icontf.localScale -= new Vector3(0.1F, 0.1F, 0);
			linetf.localScale -= new Vector3(0.1F, 0.1F, 0);
			texttf.localScale -= new Vector3(0.175f/20f, 0.175f/20f, 0);
			imagetf.localScale -= new Vector3(0.1F, 0.1F, 0);

			//destf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (description_colorStart, Color.clear, t / duration1);

			} else {
				fadeout = false;
				t = 0f;
				this.gameObject.SetActive(false);
				icontf.localScale = new Vector3(0f, 0f, 0);
				linetf.localScale = new Vector3(0f, 0f, 0);
				texttf.localScale = new Vector3(0f, 0f, 0);
				imagetf.localScale = new Vector3(0f, 0f, 0);
			}
		//yield return new WaitForSeconds(duration);
	}
	void FadeIn ()
	{
		if (t < duration2) {
			t += Time.deltaTime;
			//icontf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,icon_colorStart,  t / duration2);
			//destf.gameObject.GetComponent<Renderer> ().material.color = Color.Lerp (Color.clear,description_colorStart, t / duration2);
			icontf.localScale += new Vector3(Time.deltaTime*6, Time.deltaTime*6, 0);
			linetf.localScale += new Vector3(Time.deltaTime*6, Time.deltaTime*6, 0);
			texttf.localScale += new Vector3(Time.deltaTime*0.7f, Time.deltaTime*0.7f, 0);
			imagetf.localScale += new Vector3(Time.deltaTime*6, Time.deltaTime*6, 0);

		} else {
			fadein = false;
			t = 0f;
			this.gameObject.SetActive(true);
			icontf.localScale = new Vector3(1.5f, 1.5f, 0);
			linetf.localScale = new Vector3(1.5f, 1.5f, 0);
			texttf.localScale = new Vector3(0.175f, 0.175f, 0);
			imagetf.localScale = new Vector3(1.5f, 1.5f, 0);
		}
		//yield return new WaitForSeconds(duration);
	}
}
