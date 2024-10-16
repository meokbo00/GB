using UnityEngine;
using UnityEngine.SceneManagement;

public class P1Deadzone : MonoBehaviour
{
    public bool isExpand;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1ball")
        {
            MultiBallController ball = collision.GetComponent<MultiBallController>();
            if (!ball.hasExpanded)
                SceneManager.LoadScene("P2 Win");
        }


        if (collision.gameObject.tag == "P1Item")
        {
            switch (collision.gameObject.name)
            {
                case "BlackHoleF(Clone)":
                    BlackHole_Skill skill = collision.GetComponent<BlackHole_Skill>();
                    this.isExpand = skill.hasExpanded;
                    break;
                case "DurabilityF(Clone)":
                    Durability_Skill skill2 = collision.GetComponent<Durability_Skill>();
                    this.isExpand = skill2.hasExpanded;
                    break;
                case "FastenF(Clone)":
                    Fasten_Skill skill3 = collision.GetComponent<Fasten_Skill>();
                    this.isExpand = skill3.hasExpanded;
                    break;
                case "ForceF(Clone)":
                    Force_Skill skill4 = collision.GetComponent<Force_Skill>();
                    this.isExpand = skill4.hasExpanded;
                    break;
                case "InvincibleF(Clone)":
                    Invincible_Skill skill5 = collision.GetComponent<Invincible_Skill>();
                    this.isExpand = skill5.hasExpanded;
                    break;
                case "Random_numberF(Clone)":
                    Random_number_Skill skill6 = collision.GetComponent<Random_number_Skill>();
                    this.isExpand = skill6.hasExpanded;
                    break;
                case "ReductionF(Clone)":
                    Reduction_Skill skill7 = collision.GetComponent<Reduction_Skill>();
                    this.isExpand = skill7.hasExpanded;
                    break;
            }
            if (isExpand == false)
            {
                SceneManager.LoadScene("P2 Win");
            }
        }
    }
}