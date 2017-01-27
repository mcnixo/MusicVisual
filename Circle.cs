using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

    public SpriteRenderer thepic;
    public GameObject prefab;
    
    public int numberOfObjects = 124;
    public float holdRot = 0;
    public float radius = 5f, peak = 0, MidPeak = 0, HighPeak = 0;
    public float changeSpeed = 0.1f;
    public GameObject[] cubes;
    public GameObject tiger, lego, arms, hands, legs, butt;
    public SpriteRenderer text;
    public SpriteRenderer picHigh;
    public SpriteRenderer picLow;
    public GameObject shirt, rndHead, rndHead2;
    public Material mat;
    public int count = 0;
    public float checker;
    bool hasBuilt = false;
    public bool spin = false;
    public bool switchArms = true;
    public bool colorAdapt = false;
    public float distance = 0;
    bool startDance = true;
    bool setup = false;
    int density = 10;
    public bool beatHit = false;
    public bool tilt = false;
    public float bodyMovement = 0;
    public float r = 0, g = 0, b = 0, rb = 0;
    bool playR = false, playG = false, playB = false, playRB = false, finishup = false;
    
    
  
    
    void Start()
    {
        
        
        
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(prefab, pos, Quaternion.identity);
        }
        cubes = GameObject.FindGameObjectsWithTag("cubes");

        thepic.GetComponent<SpriteRenderer>();
        text.GetComponent<SpriteRenderer>();
        playR = true;
    }

    void Update()
    {
        
        float[] spectrum = new float[1024]; AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Hamming);
        if (playR)
        {
            if (g > 0)
            {
                g -= changeSpeed * Time.deltaTime;
            }
            r += changeSpeed * Time.deltaTime;
            if (r > 0.6f)
            {
                playB = true;
                playR = false;

            }
        }
        if (playB)
        {
            b += changeSpeed * Time.deltaTime;
            r -= changeSpeed * Time.deltaTime;
            if (b > 0.6f)
            {
                playG = true;
                playB = false;
            }
        }
        if (playG)
        {
            b -= changeSpeed * Time.deltaTime;
            g += changeSpeed * Time.deltaTime;
            if (g > 0.6f)
            {
                playRB = true;
                playG = false;
            }
        }
        if (playRB)
        {
            if (g > 0)
            {
                g -= changeSpeed * Time.deltaTime;
            }
            if (b < 0.8f && !finishup)
            {
                r += changeSpeed * Time.deltaTime;
                b += changeSpeed * Time.deltaTime * 2;
                if (b > 0.8f)
                {
                    finishup = true;
                }
            }
            if (finishup && !playR)
            {
                r -= changeSpeed * Time.deltaTime;
                b -= changeSpeed * Time.deltaTime;
                if (b < 0.1f)
                {
                    playR = true;
                    finishup = false;
                    playRB = false;
                }
            }
        }
        float theLVL = spectrum[3] * (density);
        float MidLvl = spectrum[7] * (density);
        float highLvl = spectrum[12] * (density);
        peak -= 3f * Time.deltaTime;
        MidPeak -= 3f * Time.deltaTime;
        HighPeak -= 3f * Time.deltaTime;
        Vector3 armsCheck = arms.transform.eulerAngles;
        if (peak > 1)
        {
            rndHead.transform.Rotate(Vector3.forward * 2f * peak);
            rndHead2.transform.Rotate(Vector3.forward * -2f * peak);
        }
        rndHead.transform.Rotate(Vector3.up * 2f);
        rndHead2.transform.Rotate(Vector3.up * -2f);
        checker = armsCheck.x;
        
        if (armsCheck.x <= 85)
        {
            if(switchArms)
            {
                switchArms = false;
                
            }
            if (!switchArms)
            {
                switchArms = true;
                
            }                      
            
        }
        if(switchArms && startDance)
        {
            distance += -0.1f;
            arms.transform.Rotate(Vector3.up * 0.1f);
            hands.transform.Rotate(Vector3.up * 0.1f);
            legs.transform.Rotate(Vector3.up * -0.1f);
            butt.transform.Rotate(Vector3.up * -0.1f);
        }
        if(!switchArms && startDance)
        {
            
            arms.transform.Rotate(Vector3.up * -0.1f);
            hands.transform.Rotate(Vector3.up * -0.1f);
            legs.transform.Rotate(Vector3.up * 0.1f);
            butt.transform.Rotate(Vector3.up * 0.1f);

        }
        if(distance < -8.7f)
        {
            switchArms = false;
            distance = 0;
        }
    
        
        if (!spin)
        {
            tiger.transform.Rotate(Vector3.up * (0.5f));
            
        }
        if(spin)
        {
            tiger.transform.Rotate(Vector3.down * (0.5f));

        }
       
        if(beatHit)
        {
            if (spectrum[3] * density < 0.6f)
            {
                count++;
                
                beatHit = false;
            }

        }
        if (spectrum[3] * density > 0.8f && beatHit == false)
        {
            beatHit = true;
            if (!spin && count == 4)
            {
                spin = true;
                count = 0;
            }
            if(spin && count == 4)
            {
                spin = false;
                count = 0;


            }
        }
        if (beatHit && tilt)
        {
            tilt = false;
        }
        if(beatHit && !tilt)
        {
            tilt = true;
        }
        Vector3 tiltHead = lego.transform.eulerAngles;
        if (tilt)
            tiltHead.y = 87;
        if (!tilt)
            tiltHead.y = 94;

        if (theLVL > peak)
        {
            peak = theLVL;
        }
        if (MidLvl > MidPeak)
        {
            MidPeak = MidLvl;
        }
        if (highLvl > HighPeak)
        {
            
            HighPeak = highLvl;
        }
        
        


        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 TextpreviousScale = text.transform.localScale;
            Vector3 highPrevScale = picHigh.transform.localScale;
            Vector3 MidPrevScale = picLow.transform.localScale;
            Vector3 legoPrevScale = lego.transform.localScale;
            if (peak < 1)
            {
                TextpreviousScale.x = peak * 1.3f + 1;
                legoPrevScale.y = peak * 1.3f + 1;
            }
            if(MidPeak < 1)
            {
                
                MidPrevScale.x = MidPeak + 1;
                
            }
            if(HighPeak < 1)
            {
                highPrevScale.x = HighPeak * 1.3f + 1;
                legoPrevScale.x = HighPeak * 1.5f + 1;
                

            }
            if (peak > 1.5f)
            {
                TextpreviousScale.x = peak / 2 + 1;
                
            }
            if (MidPeak > 1.5f)
            {

                MidPrevScale.x = MidPeak / 15 + 1;
                


            }
            if (HighPeak > 1.5f)
            {
                highPrevScale.x = HighPeak / 10 + 1;
                legoPrevScale.x = HighPeak / 8 + 1;
                

            }
            if (peak > 1.5f)
            {
                TextpreviousScale.x = peak / 2 + 1;
                

            }
            if (MidPeak > 1.5f)
            {

                MidPrevScale.x = MidPeak / 15 + 1;
                
            }
            if (HighPeak > 1.5f)
            {
                highPrevScale.x = HighPeak / 10 + 1;
                
            }
            
            
            text.transform.localScale = TextpreviousScale;
            picHigh.transform.localScale = highPrevScale;
            picLow.transform.localScale = MidPrevScale;
            lego.transform.localScale = legoPrevScale;
            lego.transform.Rotate(Vector3.forward * Time.deltaTime * 1.5f);
            if (colorAdapt)
            {
                text.material.color = new Color(r, g, peak, 0.7f);

            }
            thepic.material.color = new Color(r, g, b, peak);      //spectrum[3] * (density)   <--- for bounce
            MeshRenderer shirtColor = shirt.GetComponent<MeshRenderer>();
            shirtColor.material.color = new Color(r, g, b, peak);


            Vector3 previousScale = cubes[i].transform.localScale;
            previousScale.y = spectrum[i] * 20;
            cubes[i].transform.localScale = previousScale;
            
            if(spectrum[i] * density < 0.4f)
            {
                
                changeColors(i, new Color(0, 0, spectrum[i] * 20));
                holdRot += 0.02f;
            }
            if (spectrum[i] * density > 0.4f && spectrum[i] * density < 0.7f)
            {
                changeColors(i, new Color(0, spectrum[i] * density, 0));
                holdRot += 0.2f;
            }
            if (spectrum[i] * density > 0.7f)
            {
                changeColors(i, new Color(spectrum[i] * density - 1,0, 0));
                holdRot+= 5f;
                
            }
            cubes[i].transform.rotation = Quaternion.Euler(0, holdRot, 0);

        }

        
        
    }

    public void changeColors(int theCube, Color theColor)
    {
        
            cubes[theCube].GetComponent<Renderer>().material.color = theColor;
    }
    
}
