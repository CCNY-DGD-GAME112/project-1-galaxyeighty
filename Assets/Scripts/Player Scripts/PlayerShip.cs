using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    Vector2 initialPos;

    Gun[] guns;

    float moveSpeed = 5;

    int hits = 5;
    bool invuln = false;
    float invulnTimer = 0;
    float invulnTime = 1;

    bool moveUp;
    bool moveDown;
    bool moveLeft;
    bool moveRight;
    bool speedUp;

    bool shoot;

    GameObject forcefield;

    int gunUpgradeLevel = 0;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    private void Awake()
    {
        initialPos = transform.position;
        spriteRenderer = transform.Find("playersprite").GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        forcefield = transform.Find("Forcefield").gameObject;
        DeactivateFF();
        guns = transform.GetComponentsInChildren<Gun>();
        foreach (Gun gun in guns)
        {
            gun.isActive = true;
            if (gun.upgradeLevelRequirement != 0)
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

       moveUp = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
       moveDown = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
       moveLeft = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
       moveRight = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
       speedUp = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        shoot = Input.GetKeyDown(KeyCode.Space);
        if (shoot)
        {
            foreach (Gun gun in guns)
            {
                if (gun.gameObject.activeSelf)
                {
                    gun.Shoot();
                }
            }
        }

        if (invuln)
        {
            if (invulnTimer >= invulnTime)
            {
                invulnTimer = 0;
                invuln = false;
                spriteRenderer.enabled = true;
            }
            else
            {
                invulnTimer += Time.deltaTime;
                spriteRenderer.enabled = !spriteRenderer.enabled;
            }
        }
    }

    private void FixedUpdate()
    {
        //movement script
        Vector2 pos = transform.position;

        float moveAmount = moveSpeed * Time.fixedDeltaTime;
        if (speedUp)
        {
            moveAmount *= 2;
        }
        Vector2 move = Vector2.zero;

        if (moveUp)
        {
            move.y += moveAmount;
        }

        if (moveDown)
        {
            move.y -= moveAmount;
        }

        if (moveLeft)
        {
            move.x -= moveAmount;
        }

        if (moveRight)
        {
            move.x += moveAmount;
        }

        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        Debug.Log(moveMagnitude);

        //setting boundaries
        pos += move;

        if (pos.x <= -8f)
        {
            pos.x = -8f;
        }
        if (pos.x >= 8f)
        {
            pos.x = 8f;
        }
        if (pos.y <= -4.5f)
        {
            pos.y = -4.5f;
        }
        if (pos.y >= 4.5f)
        {
            pos.y = 4.5f;
        }

        transform.position = pos;
    }

    //forcefield pickup
    void ActivateFF()
    {
        forcefield.SetActive(true);
    }

    void DeactivateFF()
    {
        forcefield.SetActive(false);
    }

    bool HasFF()
    {
        return forcefield.activeSelf;
    }

    //gun upgrades

    void AddGuns()
    {
        gunUpgradeLevel++;
        foreach(Gun gun in guns)
        {
            if (gun.upgradeLevelRequirement <= gunUpgradeLevel)
            {
                gun.gameObject.SetActive(true);    
            }
            else
            {
                gun.gameObject.SetActive(false);
            }
        }
    }

    //speed upgrade
    void SpeedBoost()
    {
        moveSpeed++;
    }

    void ResetShip()
    {
        transform.position = initialPos;
        DeactivateFF();
        gunUpgradeLevel = -1;
        AddGuns();
        moveSpeed = 5;
        hits = 5;
        Level.instance.ResetLevel();
    }

    void Hit(GameObject gameObjectHit)
    {
        if (HasFF())
        {
            DeactivateFF();
        }
        else
        {
            if (!invuln)
            {
                hits--;
                if (hits == 0)
                {
                    ResetShip();
                }
                else
                {
                    invuln = true;
                }
                Destroy(gameObjectHit);
            }
        }
    }


            //collision
           private void OnTriggerEnter2D(Collider2D collision)
            {
                GunScript projectile = collision.GetComponent<GunScript>();
                if (projectile != null)
                {
                    if (projectile.isEnemy)
                    {
                        Hit(projectile.gameObject);
                    }
                }

                EnemyDeath destroyable = collision.GetComponent<EnemyDeath>();
                if (destroyable != null)
                {
                   Hit(destroyable.gameObject);

                }

                PickupScript pickupScript = collision.GetComponent<PickupScript>();
                if (pickupScript)
                {
                    if (pickupScript.activateFF)
                    {
                        ActivateFF();
                    }
                    if (pickupScript.addGuns)
                    {
                        AddGuns();
                    }
                    if (pickupScript.speedBoost)
                    {
                        SpeedBoost();
                    }
                    Level.instance.AddScore(pickupScript.pointValue);
                    Destroy(pickupScript.gameObject);
                }
            }

}
