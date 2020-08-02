using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    public GameObject m_Ground;
    public GameObject m_Wall;
    public GameObject m_RewindBlock;
    public GameObject m_Pumpkin;
    public GameObject m_Spider;
    public GameObject m_Skeleton;
    public GameObject m_Bat;
    public GameObject m_Slime;
    public static int m_CurrentLevel=0;
    public float m_Time;
    public int m_Moves;
    public string m_Story;

    public TextAsset m_Level;
    void Start()
    {
        m_CurrentLevel++;
        string level = "Level/"+m_CurrentLevel.ToString();
        m_Level = Resources.Load<TextAsset>(level);
        LoadLevel();

    }
    private void LoadLevel()
    {
        string[] lines = m_Level.text.Split('\n');
        
        int collums=0;
        int rows=0;
        GameObject[,] ground;
        GameObject wall;
        int l= 0;

        foreach(string obj in lines)
        {
            if(obj.StartsWith("Level:"))
            {
                string[] groundsize = obj.Replace("Level:", "").Split(',');
                collums = int.Parse(groundsize[0]);
                rows = int.Parse(groundsize[1]);                
            }
            if(obj.StartsWith("Story:"))
            {
                m_Story = obj.Replace("Story:", "");
            }
            if(obj.StartsWith("Time:"))
            {
                m_Time = float.Parse(obj.Replace("Time:", ""));
            }
            if(obj.StartsWith("Moves:"))
            {
                m_Moves = int.Parse(obj.Replace("Moves:", ""));
            }

            if(obj.StartsWith("#"))
            {                
                char[] construct = obj.ToCharArray();
                for(int i=0; i < collums; i++)
                {
                    if(construct[i] == '#')
                        wall = Instantiate(m_Wall, new Vector3(i,l, 0), Quaternion.identity);
                    else if(construct[i] == '@')
                        wall = Instantiate(m_RewindBlock, new Vector3(i, l, 0), Quaternion.identity);
                    else if(construct[i] == 'P')
                        transform.position = new Vector3(i, l, 0);
                    
                }
                l++;
            }            
        }

        ground = new GameObject[rows, collums];

        for(int i=0; i< rows; i++)
        {
            for(int j=0; j< collums; j++)
            {
                ground[i, j] = Instantiate(m_Ground, new Vector3(j, i, 1), Quaternion.identity);
            }
        }
        

    }
}
