using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbility : MonoBehaviour
{
    public GameObject RightHand_g;
    private CapsuleCollider _rightHandCollider;

    private Animator _anim;
    private Rigidbody _playerRB;

    public Image A1image;
    public Image A2image;
    public Image A3image;

    public GameObject LaserPrefab;
    public Transform LaserOrigin;
    public float LaserSpeed = 40f;
    public int[] CoolDownTime = { 3, 3, 3 };
    private bool[] _abilityEnable = { true, true, true };
    public float[] CoolDownTimer = { 0, 0, 0 };

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rightHandCollider = RightHand_g.GetComponent<CapsuleCollider>();
        _playerRB = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetBool("autoAttack", true);
            StartCoroutine(attack());
        }
        else
        {
            _anim.SetBool("autoAttack", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(Ability1());
        } else
        {
            _anim.SetBool("ability1", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Ability2();
            StartCoroutine(Ability2());
        }
        else
        {
            _anim.SetBool("ability2", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Ability3();
        }
        else
        {
            _anim.SetBool("ability3", false);
        }

        for(int i = 2; i >= 0; i--)
        {
            if (CoolDownTimer[i] != 0)
            {
                CoolDownTimer[i] -= Time.deltaTime;
            }
            float normalizedfill = Mathf.Clamp(1 - CoolDownTimer[i] / CoolDownTime[i], 0.0f, 1.0f);
            switch(i)
            {
                case 0:
                    A1image.fillAmount = normalizedfill;
                    break;
                case 1:
                    A2image.fillAmount = normalizedfill;
                    break;
                case 2:
                    A3image.fillAmount = normalizedfill;
                    break;
            }
        }

    }
    IEnumerator attack()
    {
        _rightHandCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        _rightHandCollider.enabled = false;
    }

    IEnumerator Ability1()
    {
        if (_abilityEnable[0])
        {
            StartCoroutine(cooldown(1, CoolDownTime[0]));
            _anim.SetBool("ability1", true);
            yield return new WaitForSeconds(0.4f);

            GameObject laserclone;
            laserclone = Instantiate(LaserPrefab, LaserOrigin.position, LaserPrefab.transform.rotation);
            //laserclone.transform.Rotate(0, 0, 90);
            //laserclone.transform.eulerAngles = new Vector3(90, 90, 90);
            laserclone.GetComponent<Rigidbody>().velocity = this.transform.forward * LaserSpeed + new Vector3(0, 4, 0);
            laserclone.GetComponent<Rigidbody>().AddTorque(0, 1000, 0);

        }
    }

    IEnumerator Ability2()
    {
        if (_abilityEnable[1])
        {
            StartCoroutine(cooldown(2, CoolDownTime[1]));
            _anim.SetBool("ability2", true);
            _playerRB.AddForce(Vector3.up * 10, ForceMode.VelocityChange);
            yield return new WaitForSeconds(1f);
            for (int i = 5; i > 0; i--)
            {
            _playerRB.AddForce(Vector3.up * 3, ForceMode.VelocityChange);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void Ability3()
    {
        if (_abilityEnable[2])
        {
            StartCoroutine(cooldown(3, CoolDownTime[2]));
            _anim.SetBool("ability3", true);
            PlayerState.Playerhealth += 10;
        }
    }

    IEnumerator cooldown(int x, float t)
    {
        CoolDownTimer[x - 1] = 3;
        _abilityEnable[x - 1] = false;
        yield return new WaitForSeconds(t);
        _abilityEnable[x - 1] = true;
    }

}
