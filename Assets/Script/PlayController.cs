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


	// 選択したfaceアイテム
	public int[] selectFace = new int[3];

	// 設置したfoodアイテム
	public int[,] selectFoods = new int[8, 6];

	// 各アイテムのPrefab
	public GameObject facePrefab;
	public GameObject foodsPrefab;
	public GameObject foodsBackGroundPrefab;
	public GameObject basePrefab;
	public GameObject bentoPrefab;

	// faceアイテムの座標（１アイテムごとに可変のため）
	public Vector3[] facePosition = new Vector3[12];	// 4×3個

	// faceアイテムのサイズ（１アイテムごとに可変のため）
	public Vector3[] faceSize = new Vector3[12];		// 4×3個

	// 選択中のアイテム
	private int currentItem;

	// 現在の状態
	public int state = -1;

	private int selectCnt = 0;
	private int sizeSCnt = 0;
	private GameObject currentObj;
	private GameObject bentoObj;
	private GameObject bentoObj2;
	private GameObject baseObj;
	private Vector2 baseOffset = new Vector2(-2.92f, 1.91f);

	private Vector3 mousePosition;
	private Vector3 preMousePosition;

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

		// フェイス位置
		facePosition [0] = new Vector3 (-1.59f, -0.86f, 0.0f);
		facePosition [1] = new Vector3 (-1.59f, -0.86f, 0.0f);
		facePosition [2] = new Vector3 (-1.59f, -0.86f, 0.0f);
		facePosition [3] = new Vector3 (-1.59f, -0.86f, 0.0f);
		facePosition [4] = new Vector3 (-1.59f, -1.75f, 0.0f);
		facePosition [5] = new Vector3 (-1.59f, -1.75f, 0.0f);
		facePosition [6] = new Vector3 (-1.59f, -1.75f, 0.0f);
		facePosition [7] = new Vector3 (-1.59f, -1.75f, 0.0f);
		facePosition [8] = new Vector3 (-1.38f, 0.25f, 0.0f);
		facePosition [9] = new Vector3 (-0.85f, 0.17f, 0.0f);
		facePosition [10] = new Vector3 (-1.58f, 0.49f, 0.0f);
		facePosition [11] = new Vector3 (-0.81f, 0.15f, 0.0f);

		// フェイスサイズ
		faceSize [0] = new Vector3 (1.2f, 1.2f, 0.0f);
		faceSize [1] = new Vector3 (1.2f, 1.2f, 0.0f);
		faceSize [2] = new Vector3 (1.2f, 1.2f, 0.0f);
		faceSize [3] = new Vector3 (1.2f, 1.2f, 0.0f);
		faceSize [4] = new Vector3 (0.8f, 0.8f, 0.0f);
		faceSize [5] = new Vector3 (0.8f, 0.8f, 0.0f);
		faceSize [6] = new Vector3 (0.8f, 0.8f, 0.0f);
		faceSize [7] = new Vector3 (0.8f, 0.8f, 0.0f);
		faceSize [8] = new Vector3 (1.7f, 1.2f, 0.0f);
		faceSize [9] = new Vector3 (0.8f, 0.8f, 0.0f);
		faceSize [10] = new Vector3 (0.9f, 0.9f, 0.0f);
		faceSize [11] = new Vector3 (0.8f, 0.8f, 0.0f);

	}
	
	// Update is called once per frame
	void Update () {
		// 何もしないステート
		if (state == -1) {
			return;
		}
		// アイテム選択ステート
		if (state == 0) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast ((Vector2)ray.origin, (Vector2)ray.direction, 100);
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag == "FoodObj") {

						// カレントオブジェクトに指定
						currentObj = hit.collider.gameObject;
						currentObj.transform.parent = bentoObj.transform;
						currentObj.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));

						// スケール変更
						currentItem = int.Parse (currentObj.name.Substring (currentObj.name.Length - 9, 2)); // play@face00(Clone)から00を取り出す
						if (selectCnt <= 2) {
							selectFace [selectCnt] = currentItem;
							currentObj.transform.localScale = faceSize [selectFace [selectCnt]];
						} else if (selectCnt == 3) {
							currentObj.transform.localScale = new Vector3(1.8f, 1.8f, transform.localScale.z);
						} else if (selectCnt == 4) {
							currentObj.transform.localScale = new Vector3(1.6f, 1.6f, transform.localScale.z);
						}

						// 選択されなかったおかずもろとも削除
						if (baseObj != null) {
							GameObject.Destroy (baseObj);
						}

						// ドラッグの初期座標取得
						preMousePosition = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));

						state = 1;
					}
				}
				return;
			}
		}
		// アイテム設置ステート
		if (state == 1) {

			// ドラッグ
			mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
			currentObj.transform.Translate (mousePosition - preMousePosition);
			preMousePosition = mousePosition;

			// ドロップ
			if (Input.GetMouseButtonUp (0)) {

				// 設置可能か判定
				Vector2 areaOffset = new Vector2(baseOffset.x - 0.42f, baseOffset.y + 0.42f);
				Vector2 areaSize = new Vector2(0.84f * 8.0f, -0.84f * 6.0f);
				if (selectCnt <= 2) {			// 顔
					if ((mousePosition.x >= areaOffset.x) && (mousePosition.x <= areaOffset.x + areaSize.x) && (mousePosition.y <= areaOffset.y) && (mousePosition.y >= areaOffset.y + areaSize.y)) {
						// しかるべき場所に設置
						currentObj.transform.position = facePosition [selectFace [selectCnt]];
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						selectCnt++;
						Show (selectCnt);
						return;
					}
				} else if (selectCnt == 3) {	// おかず大
					// 設置
					if (SetObj()) {
						if (!IsEnableSetAfter ()) {
							selectCnt++;

							// 中おかずの置くスペースがない可能性
							if (!IsEnableSetAfter ()) {
								selectCnt++;

								// この時点での空き個数
								sizeSCnt = GetEmptyCount ();
								print ("sizeSCnt = " + sizeSCnt);
							}
						}
						Show (selectCnt);
						return;
					}
				} else if (selectCnt == 4) {	// おかず中
					// 設置
					if (SetObj()) {
						if (!IsEnableSetAfter ()) {
							selectCnt++;

							// この時点での空き個数
							sizeSCnt = GetEmptyCount ();
							print ("sizeSCnt = " + sizeSCnt);
						}
						Show (selectCnt);
						return;
					}
				} else if (selectCnt == 5) {	// おかず小
					// 設置
					if (SetObj()) {
						print ("selectCnt = " + selectCnt);
						if (!IsEnableSetAfter ()) {
							selectCnt++;
						}
						Show (selectCnt);
						return;
					}
				} else if (selectCnt == 6) {	// デザート
					// 設置
					if (SetObj()) {
						if (!IsEnableSetAfter ()) {
							DeleteEmpty ();
							state = 2;

							// とりま
							if (bentoObj != null) {
								GameObject.Destroy (bentoObj);
							}

							return;
						}
						Show (selectCnt);
						return;
					}
				}
				state = 0;
			}
		}

	}

	// 現在のマス目でおかずが設置可能か？
	public bool SetObj () {

		// 現在のマウスポジションがどのマス目か判断
		Vector2 areaOffset = new Vector2(baseOffset.x - 0.42f, baseOffset.y + 0.42f);
		Vector2 areaSize = new Vector2(0.84f * 8.0f, -0.84f * 6.0f);
		int x = -1;
		int y = -1;
		for (int yy = 0; yy < 6; yy++) {
			for (int xx = 0; xx < 8; xx++) {
				if ((mousePosition.x >= (float)xx * 0.84f + areaOffset.x) && (mousePosition.x <= (float)(xx + 1) * 0.84f + areaOffset.x) && 
					(mousePosition.y <= -(float)yy * 0.84f + areaOffset.y) && (mousePosition.y >= -(float)(yy + 1) * 0.84f + areaOffset.y)) {
					x = xx;
					y = yy;
				}
			}
		}
		// マス外
		if ((x == -1) || (y == -1)) {
			return false;
		}

		// 3×2のfoodsが設置可能か？
		if (selectCnt == 3) {
			try {
				if ((x + 2 < 8) && (y + 1 < 6) && 
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x + 2, y + 0] == -1) &&
					(selectFoods [x + 0, y + 1] == -1) &&
					(selectFoods [x + 1, y + 1] == -1) &&
					(selectFoods [x + 2, y + 1] == -1)) {

					float posx = (float)x * 0.84f + 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f - 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){
						// マス使用済み
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;
						selectFoods [x + 2, y + 0] = currentItem;
						selectFoods [x + 0, y + 1] = currentItem;
						selectFoods [x + 1, y + 1] = currentItem;
						selectFoods [x + 2, y + 1] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 1 >= 0) && (x + 1 < 8) && (y + 1 < 6) && 
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x - 1, y + 1] == -1) &&
					(selectFoods [x + 0, y + 1] == -1) &&
					(selectFoods [x + 1, y + 1] == -1)) {

					float posx = (float)x * 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f - 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){
						
						// マス使用済み
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;
						selectFoods [x - 1, y + 1] = currentItem;
						selectFoods [x + 0, y + 1] = currentItem;
						selectFoods [x + 1, y + 1] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 2 >= 0) && (y + 1 < 6) && 
					(selectFoods [x - 2, y + 0] == -1) &&
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x - 2, y + 1] == -1) &&
					(selectFoods [x - 1, y + 1] == -1) &&
					(selectFoods [x + 0, y + 1] == -1)) {

					float posx = (float)x * 0.84f - 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f - 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x - 2, y + 0] = currentItem;
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x - 2, y + 1] = currentItem;
						selectFoods [x - 1, y + 1] = currentItem;
						selectFoods [x + 0, y + 1] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x + 2 < 8) && (y - 1 >= 0) && 
					(selectFoods [x + 0, y - 1] == -1) &&
					(selectFoods [x + 1, y - 1] == -1) &&
					(selectFoods [x + 2, y - 1] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x + 2, y + 0] == -1)) {

					float posx = (float)x * 0.84f + 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f + 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x + 0, y - 1] = currentItem;
						selectFoods [x + 1, y - 1] = currentItem;
						selectFoods [x + 2, y - 1] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;
						selectFoods [x + 2, y + 0] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 1 >= 0) && (x + 1 < 8) && (y - 1 >= 0) && 
					(selectFoods [x - 1, y - 1] == -1) &&
					(selectFoods [x + 0, y - 1] == -1) &&
					(selectFoods [x + 1, y - 1] == -1) &&
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1)) {

					float posx = (float)x * 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f + 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x - 1, y - 1] = currentItem;
						selectFoods [x + 0, y - 1] = currentItem;
						selectFoods [x + 1, y - 1] = currentItem;
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 2 >= 0) && (y - 1 >= 0) && 
					(selectFoods [x - 2, y - 1] == -1) &&
					(selectFoods [x - 1, y - 1] == -1) &&
					(selectFoods [x + 0, y - 1] == -1) &&
					(selectFoods [x - 2, y + 0] == -1) &&
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1)) {

					float posx = (float)x * 0.84f - 0.84f + baseOffset.x;
					float posy = -(float)y * 0.84f + 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x - 2, y - 1] = currentItem;
						selectFoods [x - 1, y - 1] = currentItem;
						selectFoods [x + 0, y - 1] = currentItem;
						selectFoods [x - 2, y + 0] = currentItem;
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
		}
		// 2×2のfoodsが設置可能か？
		if (selectCnt == 4) {
			try {
				if ((x + 1 < 8) && (y + 1 < 6) && 
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 1] == -1) &&
					(selectFoods [x + 1, y + 1] == -1)) {

					float posx = (float)x * 0.84f + 0.42f + baseOffset.x;
					float posy = -(float)y * 0.84f - 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 1] = currentItem;
						selectFoods [x + 1, y + 1] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 1 >= 0) && (y + 1 < 6) && 
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x - 1, y + 1] == -1) &&
					(selectFoods [x + 0, y + 1] == -1)) {

					float posx = (float)x * 0.84f - 0.42f + baseOffset.x;
					float posy = -(float)y * 0.84f - 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x - 1, y + 1] = currentItem;
						selectFoods [x + 0, y + 1] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x + 1 < 8) && (y - 1 >= 0) && 
					(selectFoods [x + 0, y - 1] == -1) &&
					(selectFoods [x + 1, y - 1] == -1) &&
					(selectFoods [x + 0, y + 0] == -1) &&
					(selectFoods [x + 1, y + 0] == -1)) {

					float posx = (float)x * 0.84f + 0.42f + baseOffset.x;
					float posy = -(float)y * 0.84f + 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x + 0, y - 1] = currentItem;
						selectFoods [x + 1, y - 1] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;
						selectFoods [x + 1, y + 0] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
			try {
				if ((x - 1 >= 0) && (y - 1 >= 0) && 
					(selectFoods [x - 1, y - 1] == -1) &&
					(selectFoods [x + 0, y - 1] == -1) &&
					(selectFoods [x - 1, y + 0] == -1) &&
					(selectFoods [x + 0, y + 0] == -1)) {

					float posx = (float)x * 0.84f - 0.42f + baseOffset.x;
					float posy = -(float)y * 0.84f + 0.42f + baseOffset.y;
					if ((Mathf.Abs(mousePosition.x) - Mathf.Abs(posx) <= 0.42f) && (Mathf.Abs(mousePosition.y) - Mathf.Abs(posy) <= 0.42f)){

						// マス使用済み
						selectFoods [x - 1, y - 1] = currentItem;
						selectFoods [x + 0, y - 1] = currentItem;
						selectFoods [x - 1, y + 0] = currentItem;
						selectFoods [x + 0, y + 0] = currentItem;

						// しかるべき場所に設置
						currentObj.transform.position = new Vector3(posx, posy, 0.0f);
						currentObj.GetComponent<BoxCollider2D>().enabled = false; // もう動かせない
						return true;
					}
				}
			} catch(System.IndexOutOfRangeException ex) {}
		}
		// 1×1のfoodsが設置
		if ((selectCnt == 5) || (selectCnt == 6)) {
			if (selectFoods [x, y] == -1) {

				// マス使用済み
				selectFoods [x, y] = currentItem;

				// しかるべき場所に設置
				currentObj.transform.position = new Vector3 ((float)x * 0.84f + baseOffset.x, -(float)y * 0.84f + baseOffset.y, 0.0f);
				currentObj.GetComponent<BoxCollider2D> ().enabled = false; // もう動かせない
				return true;
			}
		}
		return false;
	}

	// おかずが設置可能なエリアが残っているか？
	public bool IsEnableSetAfter () {
		// 3×2のfoodsが設置可能か？
		if (selectCnt == 3) {
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
		}
		// 2×2のfoodsが設置可能か？
		if (selectCnt == 4) {
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
		}
		// まだおかずを置くか？
		if (selectCnt == 5) {
			print ("GetEmptyCount() = " + GetEmptyCount());
			if (GetEmptyCount() >= sizeSCnt / 2) {
				return true;
			}
		}
		// 1×1のfoodsが設置可能か？
		if (selectCnt == 6) {
			for (int y = 0; y < 6; y++) {
				for (int x = 0; x < 8; x++) {
					if (selectFoods [x, y] == -1)
						return true;
				}
			}
		}
		return false;
	}

	// 1×1のfoodsが何個設置可能か？（この半分の数を設置したらデザートに切り替わる）
	public int GetEmptyCount () {
		int cnt = 0;
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				if (selectFoods [x, y] == -1) {
					cnt++;
				}
			}
		}
		return cnt;
	}

	// 表示
	public void ShowFoods () {

		GameObject faceObj0 = (GameObject)Instantiate (facePrefab.transform.GetChild (selectFace[0]).gameObject, facePosition[selectFace[0]], Quaternion.identity);
		faceObj0.transform.localScale =  faceSize[selectFace[0]];
		GameObject faceObj1 = (GameObject)Instantiate (facePrefab.transform.GetChild (selectFace[1]).gameObject, facePosition[selectFace[1]], Quaternion.identity);
		faceObj1.transform.localScale =  faceSize[selectFace[1]];
		GameObject faceObj2 = (GameObject)Instantiate (facePrefab.transform.GetChild (selectFace[2]).gameObject, facePosition[selectFace[2]], Quaternion.identity);
		faceObj2.transform.localScale =  faceSize[selectFace[2]];

		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				if (selectFoods [x, y] == -99) {
					;
				} else if (selectFoods [x, y] == -1) {
					GameObject obj = (GameObject)Instantiate (foodsBackGroundPrefab, new Vector3((float)x * 0.84f + baseOffset.x, -(float)y * 0.84f + baseOffset.y, 0.0f), Quaternion.identity);
					//obj.transform.parent = this.transform;
				} else if (selectFoods [x, y] < 6) {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + 0.84f + baseOffset.x, -(float)y * 0.84f - 0.42f + baseOffset.y, 0.0f), Quaternion.identity);
					obj.transform.localScale =  new Vector3(1.8f, 1.8f, transform.localScale.z);
					//obj.transform.parent = this.transform;
					selectFoods [x + 1, y] = -99;
					selectFoods [x + 2, y] = -99;
					selectFoods [x, y + 1] = -99;
					selectFoods [x + 1, y + 1] = -99;
					selectFoods [x + 2, y + 1] = -99;
				} else if (selectFoods [x, y] < 13) {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + 0.42f + baseOffset.x, -(float)y * 0.84f - 0.42f + baseOffset.y, 0.0f), Quaternion.identity);
					obj.transform.localScale =  new Vector3(1.6f, 1.6f, transform.localScale.z);
					//obj.transform.parent = this.transform;
					selectFoods [x + 1, y] = -99;
					selectFoods [x, y + 1] = -99;
					selectFoods [x + 1, y + 1] = -99;
				} else {
					GameObject obj = (GameObject)Instantiate (foodsPrefab.transform.GetChild (selectFoods [x, y]).gameObject, new Vector3((float)x * 0.84f + baseOffset.x, -(float)y * 0.84f + baseOffset.y, 0.0f), Quaternion.identity);
					//obj.transform.parent = this.transform;
				}
			}
		}
	}

	// 表示
	public void ShowEmpty () {
		DeleteEmpty ();
		bentoObj2 = (GameObject)Instantiate (bentoPrefab);
		SpriteRenderer spRenderer = bentoObj2.GetComponent<SpriteRenderer>();
		spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, 0); // 透明に
		for (int y = 0; y < 6; y++) {
			for (int x = 0; x < 8; x++) {
				if (selectFoods [x, y] == -1) {
					GameObject obj = (GameObject)Instantiate (foodsBackGroundPrefab, new Vector3((float)x * 0.84f + baseOffset.x, -(float)y * 0.84f + baseOffset.y, 0.0f), Quaternion.identity);
					obj.transform.parent = bentoObj2.transform;
				}
			}
		}
	}
	// 表示
	public void DeleteEmpty () {
		if (bentoObj2 != null) {
			GameObject.Destroy (bentoObj2);
		}
	}

	// 表示
	public void Show (int phase) {
		state = 0;
		baseObj = (GameObject)Instantiate (basePrefab);

		// 空きエリアを表示
		ShowEmpty ();

		// 目
		if (phase == 0) {
			selectCnt = 0;	// 初期化
			bentoObj = (GameObject)Instantiate (bentoPrefab);
			GameObject obj0 = (GameObject)Instantiate (facePrefab.transform.GetChild (0).gameObject, new Vector3 (-4.35f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (facePrefab.transform.GetChild (1).gameObject, new Vector3 (-4.35f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (facePrefab.transform.GetChild (2).gameObject, new Vector3 (-4.35f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (facePrefab.transform.GetChild (3).gameObject, new Vector3 (-4.35f, -2.25f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			return;
		}
		// 口
		if (phase == 1) {
			GameObject obj0 = (GameObject)Instantiate (facePrefab.transform.GetChild (4).gameObject, new Vector3 (-4.35f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (facePrefab.transform.GetChild (5).gameObject, new Vector3 (-4.35f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (facePrefab.transform.GetChild (6).gameObject, new Vector3 (-4.35f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (facePrefab.transform.GetChild (7).gameObject, new Vector3 (-4.35f, -2.25f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			return;
		}
		// 頭
		if (phase == 2) {
			GameObject obj0 = (GameObject)Instantiate (facePrefab.transform.GetChild (8).gameObject, new Vector3 (-4.35f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (facePrefab.transform.GetChild (9).gameObject, new Vector3 (-4.35f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (facePrefab.transform.GetChild (10).gameObject, new Vector3 (-4.35f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (facePrefab.transform.GetChild (11).gameObject, new Vector3 (-4.35f, -2.25f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			return;
		}
		// おかず大
		if (phase == 3) {
			GameObject obj0 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (0).gameObject, new Vector3 (-4.35f, 2.75f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (1).gameObject, new Vector3 (-4.35f, 1.65f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (2).gameObject, new Vector3 (-4.35f, 0.55f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (3).gameObject, new Vector3 (-4.35f, -0.55f, 0.0f), Quaternion.identity);
			GameObject obj4 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (4).gameObject, new Vector3 (-4.35f, -1.65f, 0.0f), Quaternion.identity);
			GameObject obj5 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (5).gameObject, new Vector3 (-4.35f, -2.75f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			obj4.transform.parent = baseObj.transform;
			obj5.transform.parent = baseObj.transform;
			return;
		}
		// おかず中
		if (phase == 4) {
			GameObject obj0 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (6).gameObject, new Vector3 (-4.85f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (7).gameObject, new Vector3 (-4.85f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (8).gameObject, new Vector3 (-4.85f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (9).gameObject, new Vector3 (-4.85f, -2.25f, 0.0f), Quaternion.identity);
			GameObject obj4 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (10).gameObject, new Vector3 (-3.85f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj5 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (11).gameObject, new Vector3 (-3.85f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj6 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (12).gameObject, new Vector3 (-3.85f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj7 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (13).gameObject, new Vector3 (-3.85f, -2.25f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			obj4.transform.parent = baseObj.transform;
			obj5.transform.parent = baseObj.transform;
			obj6.transform.parent = baseObj.transform;
			obj7.transform.parent = baseObj.transform;
			return;
		}
		// おかず小
		if (phase == 5) {
			GameObject obj0 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (14).gameObject, new Vector3 (-4.85f, 2.4f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (15).gameObject, new Vector3 (-4.85f, 1.2f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (16).gameObject, new Vector3 (-4.85f, 0.0f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (17).gameObject, new Vector3 (-4.85f, -1.2f, 0.0f), Quaternion.identity);
			GameObject obj4 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (18).gameObject, new Vector3 (-4.85f, -2.4f, 0.0f), Quaternion.identity);
			GameObject obj5 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (19).gameObject, new Vector3 (-3.85f, 2.4f, 0.0f), Quaternion.identity);
			GameObject obj6 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (20).gameObject, new Vector3 (-3.85f, 1.2f, 0.0f), Quaternion.identity);
			GameObject obj7 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (21).gameObject, new Vector3 (-3.85f, 0.0f, 0.0f), Quaternion.identity);
			GameObject obj8 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (22).gameObject, new Vector3 (-3.85f, -1.2f, 0.0f), Quaternion.identity);
			GameObject obj9 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (23).gameObject, new Vector3 (-3.85f, -2.4f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			obj4.transform.parent = baseObj.transform;
			obj5.transform.parent = baseObj.transform;
			obj6.transform.parent = baseObj.transform;
			obj7.transform.parent = baseObj.transform;
			obj8.transform.parent = baseObj.transform;
			obj9.transform.parent = baseObj.transform;
			return;
		}
		// デザート
		if (phase == 6) {
			GameObject obj0 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (24).gameObject, new Vector3 (-4.85f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj1 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (25).gameObject, new Vector3 (-4.85f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj2 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (26).gameObject, new Vector3 (-4.85f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj3 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (27).gameObject, new Vector3 (-4.85f, -2.25f, 0.0f), Quaternion.identity);
			GameObject obj4 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (28).gameObject, new Vector3 (-3.85f, 2.25f, 0.0f), Quaternion.identity);
			GameObject obj5 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (29).gameObject, new Vector3 (-3.85f, 0.75f, 0.0f), Quaternion.identity);
			GameObject obj6 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (30).gameObject, new Vector3 (-3.85f, -0.75f, 0.0f), Quaternion.identity);
			GameObject obj7 = (GameObject)Instantiate (foodsPrefab.transform.GetChild (24).gameObject, new Vector3 (-3.85f, -2.25f, 0.0f), Quaternion.identity);
			obj0.transform.parent = baseObj.transform;
			obj1.transform.parent = baseObj.transform;
			obj2.transform.parent = baseObj.transform;
			obj3.transform.parent = baseObj.transform;
			obj4.transform.parent = baseObj.transform;
			obj5.transform.parent = baseObj.transform;
			obj6.transform.parent = baseObj.transform;
			obj7.transform.parent = baseObj.transform;
			return;
		}
	}
}
