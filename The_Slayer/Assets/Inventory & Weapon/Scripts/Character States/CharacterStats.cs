using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("File in here")]
    public CharacterData_SO characterTemplateData;

    [Header("Leave empty, it will Instantiate after Start")]
    public CharacterData_SO characterData;

    public int maxHealth { get => characterData?.maxHealth ?? 0; set => characterData.maxHealth = value; }
    public int currentHealth { get => characterData?.currentHealth ?? 0; set => characterData.currentHealth = value; }

    //different kind of weapon, different place to generate weapon
    [Header("Weapon")]
    public Transform weaponSlot;
    public Transform dropPosition;

    public itemData_SO currentWeapon;
    public itemData_SO secondWeapon;

    public Transform MagazineSlot;
    private GameObject Magzine;
    private GameObject Weapon;

    void Awake()
    {
        if (characterTemplateData != null)
        {
            characterData = Instantiate(characterTemplateData);
        }
        else
        {
            Debug.Log("You missed characterTemplateData");
        }

        if(currentWeapon != null)
        {
            GenerateWeapon(currentWeapon);
        }
    }
    
    //Melee Attack
    public void MeleeAttack(CharacterStats defender)
    {
        if(currentWeapon == null)
        {
            var damage = characterData.meleeAttack;
            defender.currentHealth -= damage;
        }
    }

    //TODO takeDamage
    public void TakeDamage()
    {

    }

    public void EquipWeapon(itemData_SO weapon)
    {

        if (weapon.weaponPrefab == null)
        {
            Debug.Log("You forget to give weapon prefab in the itemData_SO");
            return;
        }

        if(currentWeapon == null)
        {
            //No Gun
            currentWeapon = weapon;
            GenerateWeapon(currentWeapon);
        }
        else if(secondWeapon == null)
        {
            //one Gun
            //Destroy(weaponSlot.GetChild(0).gameObject);
            secondWeapon = weapon;
            //currentWeapon = weapon;
            //GenerateWeapon(currentWeapon);
        }
        else
        {
            //two guns ---
            //Drop first weapon prefab on the ground
            DropWeapon();
            secondWeapon = weapon;

            //TODO:take out the second gun (add animation here if needed)
        }

        //TODO: Check Weapon Type and Setup Animation
    }

    //Drop first Weapon and if you have second weapon, you will take it out
    public void DropWeapon()
    {
        if (currentWeapon == null)
        {
            Debug.Log("Player Do Not Have Any Weapon");
            return;
        }

        //Destory Weapon On Player
        Destroy(weaponSlot.GetChild(0).gameObject);
        if(Magzine)
            Destroy(Magzine);

        DropCurve(currentWeapon.weaponOnWorld);

        //Put secondWeapon on Player's hands
        if(secondWeapon != null)
        {
            currentWeapon = null;
            EquipWeapon(secondWeapon);
            secondWeapon = null;
        }
        else
        {
            currentWeapon = null;
        }

        //TODO: Check Weapon Type and Setup Animation
    }

    private void DropCurve(GameObject weapon)
    {
        var gun = Instantiate(weapon, dropPosition.position, Quaternion.identity);

        gun.GetComponent<Rigidbody>().AddForce(GameManager.Instance.playerStats.gameObject.transform.forward * Time.deltaTime * 200f, ForceMode.Impulse);
    }

    public bool HaveGun()
    {
        return currentWeapon != null;
    }

    public string SetUpAnimation()
    {
        Debug.Log(currentWeapon.gunType.ToString());
        return currentWeapon.gunType.ToString();
    }

    //Add health to Character/Player
    public void AddHealth(int heal)
    {
        if (characterData.currentHealth + heal <= characterData.maxHealth)
        {
            currentHealth += heal;
        }
        else
        {
            currentHealth = maxHealth;
        }
        //Debug.Log("Character's Current Health is " + currentHealth);
    }

    private void GenerateWeapon(itemData_SO weapon)
    {
        Weapon = Instantiate(weapon.weaponPrefab, weaponSlot);
        Magzine = Instantiate(weapon.magazine, MagazineSlot);
        Magzine.SetActive(false);
    }

    public void StartReload()
    {
        Magzine.SetActive(true);
        Weapon.transform.Find("magazine").gameObject.SetActive(false);
    }

    public void FinishReload()
    {
        Magzine.SetActive(false);
        Weapon.transform.Find("magazine").gameObject.SetActive(true);
    }


}
