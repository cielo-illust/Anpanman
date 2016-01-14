﻿using UnityEngine;
using System.Collections;

public class Cutin : MonoBehaviour {
	public float maxAlpha = 1.0f;
	public float time = 0.0f;
	public float time1 = 0.6f;
	public float time2 = 1.0f;
	public float time3 = 0.6f;
	public float posX = -7.0f;
	public float posX1 = 1.0f;
	public float posX2 = 1.5f;
	public float posX3 = 8.5f;
	public float posY = 0.0f;
	public float posY1 = 0.0f;
	public float posY2 = 0.0f;
	public float posY3 = 0.0f;
	public float scale = 1.0f;
	public float scale1 = 1.0f;
	public float scale2 = 1.0f;
	public float scale3 = 1.0f;

	private float currentRemainTime = 0.0f;
	private SpriteRenderer spRenderer;

	private int state = -1;
	private float srcPosX = 0.0f;
	private float dstPosX = 0.0f;
	private float srcPosY = 0.0f;
	private float dstPosY = 0.0f;
	private float srcScale = 0.0f;
	private float dstScale = 0.0f;
	private float fadeTime = 0.0f;
	private float defPosX = 0.0f;
	private float defPosY = 0.0f;
	private float defScaleX = 0.0f;
	private float defScaleY = 0.0f;

	// Use this for initialization
	void Start () {
		spRenderer = GetComponent<SpriteRenderer>();

		// 基準位置
		defPosX = transform.position.x;
		defPosY = transform.position.y;

		// 基準サイズ
		defScaleX = transform.localScale.x;
		defScaleY = transform.localScale.y;

		spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, 0);
		transform.position = new Vector3(defPosX + posX, defPosY + posY, transform.position.z);
		transform.localScale =  new Vector3(defScaleX * scale, defScaleY * scale, transform.localScale.z);

		state = -1;
		srcPosX = posX;
		dstPosX = posX;
		srcPosY = posY;
		dstPosY = posY;
		srcScale = scale;
		dstScale = scale;
		fadeTime = time;
	}

	// Update is called once per frame
	void Update () {
		currentRemainTime += Time.deltaTime;
		float alpha = currentRemainTime / fadeTime;

		// 透明状態でステイ
		if (state == -1) {
			if ( currentRemainTime >= fadeTime ) {
				state = 0;
				srcPosX = posX;
				dstPosX = posX1;
				srcPosY = posY;
				dstPosY = posY1;
				srcScale = scale;
				dstScale = scale1;
				fadeTime = time1;
				currentRemainTime = 0.0f;
				return;
			}
		}
		// フェードイン
		if (state == 0) {
			if ( currentRemainTime >= fadeTime ) {
				state = 1;
				srcPosX = posX1;
				dstPosX = posX2;
				srcPosY = posY1;
				dstPosY = posY2;
				srcScale = scale1;
				dstScale = scale2;
				fadeTime = time2;
				currentRemainTime = 0.0f;
				return;
			}
			spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, alpha * maxAlpha);
			transform.position = new Vector3(defPosX + blend (srcPosX, dstPosX, alpha), defPosY + blend (srcPosY, dstPosY, alpha), transform.position.z);
			transform.localScale =  new Vector3(defScaleX * blend (srcScale, dstScale, alpha), defScaleY * blend (srcScale, dstScale, alpha), transform.localScale.z);
		}
		// ステイ
		if (state == 1) {
			if ( currentRemainTime >= fadeTime ) {
				state = 2;
				srcPosX = posX2;
				dstPosX = posX3;
				srcPosY = posY2;
				dstPosY = posY3;
				srcScale = scale2;
				dstScale = scale3;
				fadeTime = time3;
				currentRemainTime = 0.0f;
				return;
			}
			transform.position = new Vector3(defPosX + blend (srcPosX, dstPosX, alpha), defPosY + blend (srcPosY, dstPosY, alpha), transform.position.z);
			transform.localScale =  new Vector3(defScaleX * blend (srcScale, dstScale, alpha), defScaleY * blend (srcScale, dstScale, alpha), transform.localScale.z);
		}
		// フェードアウト
		if (state == 2) {
			if (currentRemainTime >= fadeTime ) {
				GameObject.Destroy (gameObject); // 残り時間が無くなったら自分自身を消滅
				return;
			}
			spRenderer.color = new Color(spRenderer.color.r, spRenderer.color.g, spRenderer.color.b, (fadeTime - alpha) * maxAlpha);
			transform.position = new Vector3(defPosX + blend (srcPosX, dstPosX, alpha), defPosY + blend (srcPosY, dstPosY, alpha), transform.position.z);
			transform.localScale =  new Vector3(defScaleX * blend (srcScale, dstScale, alpha), defScaleY * blend (srcScale, dstScale, alpha), transform.localScale.z);
		}
	}
	float blend (float src, float dst, float alpha) {
		return dst * alpha + src * (1.0f - alpha);
	}

}