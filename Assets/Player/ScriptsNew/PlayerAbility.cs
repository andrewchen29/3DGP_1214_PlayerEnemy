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
    private int _abilityState = 1;

    public Image A1image;
    public Image A2image;
    public Image A3image;

    public GameObject LaserPrefab;
    public Transform LaserOrigin;
    public float LaserSpeed = 40f;
    public int[] CoolDownTime = { 3, 3, 3 };
    private bool[] _abilityEnable = { true, true, true };
    public float[] CoolDownTimer = { 0, 3, 0 };

    private void Start()
    {
        _anim = GetComponent<Animator>();
        _rightHandCollider = RightHand_g.GetComponent<CapsuleCollider>();
        _playerRB = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _abilityState = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _abilityState = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _abilityState = 3;
        }

        if (Input.GetMouseButtonDown(0))
        {
            _anim.SetBool("autoAttack", true);
            StartCoroutine(attack());
        }
        else
        {
            _anim.SetBool("autoAttack", false);
        }

        if (_abilityState == 1 & Input.GetMouseButtonDown(1))
        {
            StartCoroutine(Ability1());
        } else
        {
            _anim.SetBool("ability1", false);
        }

        if (_abilityState == 2 & Input.GetMouseButton(1) & _abilityEnable[1])
        {
            //Ability2();
            //StartCoroutine(Ability2());
            _anim.SetBool("ability2", true);
            _playerRB.AddForce(Vector3.up * 5, ForceMode.Impulse);
            CoolDownTimer[1] -= Time.deltaTime * 5;
        }
        else
        {
            _anim.SetBool("ability2", false);
        }

        if (_abilityState == 3 & Input.GetMouseButtonDown(1))
        {
            Ability3();
        }
        else
        {
            _anim.SetBool("ability3", false);
        }


        for (int i = 2; i >= 0; i--)
        {
            if (i != 1)
            {
                if (CoolDownTimer[i] != 0)
                {
                    CoolDownTimer[i] -= Time.deltaTime;
                }
                float normalizedfill = Mathf.Clamp(1 - CoolDownTimer[i] / CoolDownTime[i], 0.0f, 1.0f);
                switch (i)
                {
                    case 0:
                        A1image.fillAmount = normalizedfill;
                        break;
                    case 2:
                        A3image.fillAmount = normalizedfill;
                        break;
                }
            }
            else
            {
                if (CoolDownTimer[i] < 0)
                {
                    StartCoroutine(cooldown(2, 3f));
                }
                else
                {
                    if (CoolDownTimer[i] <= 3)
                    {
                        CoolDownTimer[i] += Time.deltaTime;
                    }
                }
                float normalizedfill = Mathf.Clamp(CoolDownTimer[i] / CoolDownTime[i], 0.0f, 1.0f);
                A2image.fillAmount = normalizedfill;
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
        if (x != 2)
        {
            CoolDownTimer[x - 1] = 3;
        }
        else
        {
            CoolDownTimer[x - 1] = 0;
        }
        _abilityEnable[x - 1] = false;
        yield return new WaitForSeconds(t);
        _abilityEnable[x - 1] = true;
    }

}
