using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float startSize = 1f;
    public float startSpeed = 5f;
    public float sizeModifier = .1f;

    private Rigidbody2D rigid;
    // Start is called before the first frame update
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        // Get Positions
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Appply Velocity
        Vector3 movement = Vector2.MoveTowards(transform.position, mousePos, startSpeed * Time.deltaTime);
        rigid.MovePosition(movement);

        // Filter the Position of Player to stay within Background
        transform.position = Game.Instance.GetAdjustedPosition(transform.position);
    }
    //Food will call this functiion
    private void OnTriggerEnter2D(Collider2D other)
    {
        // If colliding with Food
        if (other.name.Contains("Food"))
        {
            // Groe a little bit
            Grow();
            // Destroy Food
            Destroy(other.gameObject);
        }
    }
    public void Grow()
    {
        //Increase Player Size
        transform.localScale += new Vector3(sizeModifier, sizeModifier, sizeModifier);
        //Increase Camera scale with Player
        Game.Instance.cam.orthographicSize += sizeModifier * 5;
    }
    // Directly modify current scale
    public void Scale(float size)
    {
        transform.localScale = new Vector3(size, size, size);
    }
}
