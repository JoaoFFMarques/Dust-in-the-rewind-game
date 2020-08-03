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
        gameObject.GetComponent<PlayerController>().Record();
    }
    private void LoadLevel()
    {
        string[] lines = m_Level.text.Split('\n');
        
        int collums=0;
        int rows=0;
        GameObject[,] ground;
        GameObject build;
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

                    switch(construct[i])
                    {
                        case '#':
                            build = Instantiate(m_Wall, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case '@':
                            build = Instantiate(m_RewindBlock, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case 'S':
                            build = Instantiate(m_Slime, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case 'M':
                            build = Instantiate(m_Bat, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case 'A':
                            build = Instantiate(m_Spider, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case 'E':
                            build = Instantiate(m_Skeleton, new Vector3(i, l, 0), Quaternion.identity);
                            break;
                        case 'P':
                            transform.position = new Vector3(i, l, 0);
                            break;
                    }                    
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
