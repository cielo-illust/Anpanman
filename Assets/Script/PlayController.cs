using UnityEngine;
using System.Collections;

public class PlayController : MonoBehaviour {

	// お題（カテゴリーごとにランダム）
	public int fab1;
	public int fab2;
	public int fab3;

	// お題のオブジェクト
	public GameObject fabObj1;
	public GameObject fabObj2;
	public GameObject fabObj3;

	// お題を当てたかどうかのフラグ
	public bool fabFlag1 = false;
	public bool fabFlag2 = false;
	public bool fabFlag3 = false;

	// 選択中のアイテム
	public int thisItem;

	// 選択したfaceアイテム
	public int selectFace0;
	public int selectFace1;
	public int selectFace2;

	// 設置したfoodアイテム
	public int[,] selectFoods = new int[8, 6];

	// 各アイテムのPrefab
	public GameObject facePrefab;
	public GameObject foodsPrefab;
	public GameObject foodsBackGroundPrefab;

	// faceアイテムの座標（１アイテムごとに可変のため）
	public Vector3[] facePosition;	// 4×3個

	// faceアイテムのサイズ（１アイテムごとに可変のため）
	public Vector3[] faceSize;		// 4×3個

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this); // シーンが変わっても消えないオブジェクトに指定

		// お題選択
		fab1 = Random.Range (0, 6);
		fab2 = Random.Range (6, 8);
		fab3 = Random.Range (13, 18);

		// お題のオブジェクト取得
		fabObj1 = foodsPrefab.transform.GetChild (fab1).gameObject;
		fabObj2 = foodsPrefab.transform.GetChild (fab2).gameObject;
		fabObj3 = foodsPrefab.transform.GetChild (fab3).gameObject;

		// 初期化
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				selectFoods [x, y] = -1;
			}
		}

		// 初期に埋める
		selectFoods[0, 2] = -99;
		selectFoods[1, 2] = -99;
		selectFoods[2, 2] = -99;
		selectFoods[3, 2] = -99;
		selectFoods[0, 3] = -99;
		selectFoods[1, 3] = -99;
		selectFoods[2, 3] = -99;
		selectFoods[3, 3] = -99;
		selectFoods[0, 4] = -99;
		selectFoods[1, 4] = -99;
		selectFoods[2, 4] = -99;
		selectFoods[3, 4] = -99;
		selectFoods[0, 5] = -99;
		selectFoods[1, 5] = -99;
		selectFoods[2, 5] = -99;
		selectFoods[3, 5] = -99;

		// 値を入れてみる
		selectFoods[1, 0] = fab1;
		selectFoods[6, 1] = fab2;
		selectFoods[6, 4] = fab3;
	}
	
	// Update is called once per frame
	void Update () {

	}

	// 3×2のfoodsが設置可能か？
	public bool isSet0 () {
		for (int y = 0; y < 6 - 1; y++) {
			for (int x = 0; x < 8 - 2; x++) {
				if ((selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x + 2, y + 0] == -1) &&
					(selectFoods [x + 0, y + 1] == -1) &&
					(selectFoods [x + 1, y + 1] == -1) &&
					(selectFoods [x + 2, y + 1] == -1)) {
					return true;
				}
			}
		}
		return false;
	}

	// 2×2のfoodsが設置可能か？
	public bool isSet2 () {
		for (int y = 0; y < 6 - 1; y++) {
			for (int x = 0; x < 8 - 1; x++) {
				if ((selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 1] == -1) &&
					(selectFoods [x + 1, y + 1] == -1)) {
					return true;
				}
			}
		}
		return false;
	}

	// 1×1のfoodsが何個設置可能か？（この半分の数を設置したらデザートに切り替わる）
	public int getCount(){
		int cnt = 0;
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				if (selectFoods [x + 0, y + 0] == -1) {
					cnt++;
				}
			}
		}
		return cnt;
	}

	// 表示
	public void ShowFoods () {
		Vector2 offset = new Vector2(-2.92f, 2.56f);
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				if (selectFoods [x, y] == -99) {
					;
				} else if (selectFoods [x, y] == -1) {
					GameObject obj = (GameObject)Instantiate (foodsBackGroundPrefab, new Vector3((float)x * 0.84f + offset.x, -(float)y * 0.84f + offset.y, 0.0f), Quaternion.identity);
					//obj.transform.parent = this.transform;
				} else if (selectFoods [x, y] < 6) {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + 0.84f + offset.x, -(float)y * 0.84f - 0.42f + offset.y, 0.0f), Quaternion.identity);
					obj.transform.localScale =  new Vector3(1.8f, 1.8f, transform.localScale.z);
					//obj.transform.parent = this.transform;
					selectFoods [x + 1, y] = -99;
					selectFoods [x + 2, y] = -99;
					selectFoods [x, y + 1] = -99;
					selectFoods [x + 1, y + 1] = -99;
					selectFoods [x + 2, y + 1] = -99;
				} else if (selectFoods [x, y] < 13) {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + 0.42f + offset.x, -(float)y * 0.84f - 0.42f + offset.y, 0.0f), Quaternion.identity);
					obj.transform.localScale =  new Vector3(1.6f, 1.6f, transform.localScale.z);
					//obj.transform.parent = this.transform;
					selectFoods [x + 1, y] = -99;
					selectFoods [x, y + 1] = -99;
					selectFoods [x + 1, y + 1] = -99;
				} else {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + offset.x, -(float)y * 0.84f + offset.y, 0.0f), Quaternion.identity);
					//obj.transform.parent = this.transform;
				}
			}
		}
	}
}
