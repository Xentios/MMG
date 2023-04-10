using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageAnimation : MonoBehaviour
{

	public Sprite[] sprites;
	public int spritePerFrame = 6;
	public bool loop = true;
	public bool destroyOnEnd = false;
	public bool move = false;
	public float movement_offset = 1;


	private int index = 0;
	private Image image;
	private int frame = 0;
	private Vector3 startingPosition;

	void Awake()
	{
		image = GetComponent<Image>();
		
	}
    private void Start()
    {
		startingPosition = image.gameObject.transform.position;
		spritePerFrame = 6;
	}


    private void FixedUpdate()
    {
		if (!loop && index == sprites.Length) return;
		frame++;
		if (frame < spritePerFrame) return;
		image.sprite = sprites[index];
		frame = 0;
		index++;
		if (move) MyEdit(index);
		if (index >= sprites.Length)
		{
			if (move) MyEditEnd();
			if (loop) index = 0;
			if (destroyOnEnd) Destroy(gameObject);

		}
	}



    private void MyEdit(int index)
    {
		var pos = image.gameObject.transform.position;
		image.gameObject.transform.position = new Vector3(pos.x, pos.y + movement_offset, pos.z);
	}
	private void MyEditEnd()
    {
		var pos = image.gameObject.transform.position;
		image.gameObject.transform.position = new Vector3(pos.x, pos.y - index * movement_offset, pos.z);
	}
}
