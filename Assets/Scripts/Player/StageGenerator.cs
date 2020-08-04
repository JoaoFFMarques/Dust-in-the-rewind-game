using UnityEngine;

public class StageGenerator : MonoBehaviour
{
    [Header("Player")]
    public GameObject m_Player;
    
    [Header("Ground")]
    public GameObject m_Ground;
    public GameObject m_LeftGround;
    public GameObject m_RightGround;
    public GameObject m_UpperGround;
    public GameObject m_LowerGround;
    public GameObject m_UpperLeftGround;
    public GameObject m_UpperRightGround;
    public GameObject m_LowerLeftGround;
    public GameObject m_LowerRightGround;
    
    [Header("Wall")]
    public GameObject m_Wall;
    public GameObject m_LeftWall;
    public GameObject m_RightWall;
    public GameObject m_UpperLeftWall;
    public GameObject m_UpperRightWall;
    public GameObject m_LowerLeftWall;
    public GameObject m_LowerRightWall;

    [Header("Obstacle")]
    public GameObject m_RewindBlock;

    [Header("Enemies")]
    public GameObject m_Pumpkin;
    public GameObject m_Slime;
    public GameObject m_Bat;
    public GameObject m_Spider;
    public GameObject m_Skeleton;
    public GameObject m_Ghost;    
    
    [Header("Level Infos")]
    public float m_Time;
    public int m_Moves;
    public string m_Story;
    public TextAsset m_Level;

    public static int m_CurrentLevel = 0;

    void Start()
    {
        m_CurrentLevel++;
        string level = "Level/"+m_CurrentLevel.ToString();
        
        m_Level = Resources.Load<TextAsset>(level);
       // LoadLevel();
    }
    public void LoadLevel()
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

                    if(construct[i]=='#' && i == 0 && l==0)
                        build = Instantiate(m_LowerLeftWall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && (i > 0 && i < collums-1) && l == 0)
                        build = Instantiate(m_Wall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && i == collums - 1 && l == 0)
                        build = Instantiate(m_LowerRightWall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && i == 0 && (l > 0 && l < rows-1))
                        build = Instantiate(m_LeftWall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && i == collums - 1 && (l > 0 && l < rows - 1))
                        build = Instantiate(m_RightWall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && i == collums-1 && l == rows-1)
                        build = Instantiate(m_UpperRightWall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && (i > 0 && i < collums-1) && l == rows-1)
                        build = Instantiate(m_Wall, new Vector2(i, l), Quaternion.identity, transform);
                    else if(construct[i] == '#' && i == 0 && l == rows-1)
                        build = Instantiate(m_UpperLeftWall, new Vector2(i, l), Quaternion.identity, transform);
                    

                    switch(construct[i])
                    {
                        case '@':
                            build = Instantiate(m_RewindBlock, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'B':
                            build = Instantiate(m_Pumpkin, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'S':
                            build = Instantiate(m_Slime, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'M':
                            build = Instantiate(m_Bat, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'A':
                            build = Instantiate(m_Spider, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'E':
                            build = Instantiate(m_Skeleton, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'G':
                            build = Instantiate(m_Ghost, new Vector2(i, l), Quaternion.identity, transform);
                            break;
                        case 'P':
                            build = Instantiate(m_Player, new Vector2(i, l), Quaternion.identity);
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
                if(j==0 && i==0)
                    ground[i, j] = Instantiate(m_LowerLeftGround, new Vector2(j, i), Quaternion.identity, transform);
                else if((j > 0 && j < collums - 1) && i == 0)
                    ground[i, j] = Instantiate(m_LowerGround, new Vector2(j, i), Quaternion.identity, transform);
                else if(j == collums - 1 && i == 0)
                    ground[i, j] = Instantiate(m_LowerRightGround, new Vector2(j, i), Quaternion.identity, transform);
                else if(j == 0 && (i > 0 && i<rows-1))
                    ground[i, j] = Instantiate(m_LeftGround, new Vector2(j, i), Quaternion.identity, transform);
                else if(j == collums-1 && (i > 0 && i < rows - 1))
                    ground[i, j] = Instantiate(m_RightGround, new Vector2(j, i), Quaternion.identity, transform);
                else if(j == 0  && i == rows-1)
                    ground[i, j] = Instantiate(m_UpperLeftGround, new Vector2(j, i), Quaternion.identity, transform);
                else if((j > 0 && j < collums - 1) && i == rows-1)
                    ground[i, j] = Instantiate(m_UpperGround, new Vector2(j, i), Quaternion.identity, transform);
                else if(j == collums - 1 && i == rows-1)
                    ground[i, j] = Instantiate(m_UpperRightGround, new Vector2(j, i), Quaternion.identity, transform);
                else
                    ground[i, j] = Instantiate(m_Ground, new Vector2(j, i), Quaternion.identity, transform);
              
            }
        }
       
    }
}
