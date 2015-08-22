using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

[AddComponentMenu("Event/Alpha Raycaster"), ExecuteInEditMode]
public class AlphaRaycaster : GraphicRaycaster
{
	[Header("Alpha test properties")]
	[Range(0, 1), Tooltip("Below that value of alpha components won't react to raycast.")]
	public float AlphaThreshold = .9f;
	[Tooltip("Include material tint color when checking alpha.")]
	public bool IncludeMaterialAlpha;
	[Tooltip("Will test alpha only on objects with Alpha Check component.")]
	public bool SelectiveMode;
	[Tooltip("Show warnings in the console when raycasting objects with a not-readable texture.")]
	public bool ShowTextureWarnings;

	private List<RaycastResult> toExclude = new List<RaycastResult>();

	protected override void OnEnable ()
	{
		base.OnEnable();

		var badGuy = GetComponent<GraphicRaycaster>();
		if (badGuy && badGuy != this) DestroyImmediate(badGuy);
	}

	public override void Raycast (PointerEventData eventData, List<RaycastResult> resultAppendList)
	{
		base.Raycast(eventData, resultAppendList);

		toExclude.Clear();

		foreach (var result in resultAppendList)
		{
			var objImage = result.gameObject.GetComponent<Image>();
			if (!objImage) continue;

			var objAlphaCheck = result.gameObject.GetComponent<AlphaCheck>();
			if (SelectiveMode && !objAlphaCheck) continue;

			try
			{
				var objTrs = result.gameObject.transform as RectTransform;

				// evaluating pointer position relative to object local space
				Vector3 pointerGPos;
				if (eventCamera)
				{
					var objPlane = new Plane(objTrs.forward, objTrs.position);
					float distance;
					var cameraRay = eventCamera.ScreenPointToRay(eventData.position);
					objPlane.Raycast(cameraRay, out distance);
					pointerGPos = cameraRay.GetPoint(distance);
				}
				else
				{
					pointerGPos = eventData.position;
					float rotationCorrection = (-objTrs.forward.x * (pointerGPos.x - objTrs.position.x) - objTrs.forward.y * (pointerGPos.y - objTrs.position.y)) / objTrs.forward.z;
					pointerGPos += new Vector3(0, 0, objTrs.position.z + rotationCorrection);
				}
				Vector3 pointerLPos = objTrs.InverseTransformPoint(pointerGPos);

				// obtaining object's texture and evaluating texture coordinates of the targeted spot
				var objTex = objImage.mainTexture as Texture2D;
				var texRect = objImage.sprite.textureRect;
				float texCorX = pointerLPos.x * (texRect.width / objTrs.sizeDelta.x) + texRect.width * objTrs.pivot.x;
				float texCorY = pointerLPos.y * (texRect.height / objTrs.sizeDelta.y) + texRect.height * objTrs.pivot.y;

				// for filled images, check if targeted spot is outside of the filled area
				#region Filled
				if (objImage.type == Image.Type.Filled)
				{
					var nCorX = texRect.height > texRect.width ? texCorX * (texRect.height / texRect.width) : texCorX;
					var nCorY = texRect.width > texRect.height ? texCorY * (texRect.width / texRect.height) : texCorY;
					var nWidth = texRect.height > texRect.width ? texRect.height : texRect.width;
					var nHeight = texRect.width > texRect.height ? texRect.width : texRect.height;

					if (objImage.fillMethod == Image.FillMethod.Horizontal)
					{
						if (objImage.fillOrigin == (int)Image.OriginHorizontal.Left && texCorX / texRect.width > objImage.fillAmount) { toExclude.Add(result); continue; }
						if (objImage.fillOrigin == (int)Image.OriginHorizontal.Right && texCorX / texRect.width < (1 - objImage.fillAmount)) { toExclude.Add(result); continue; }
					}

					if (objImage.fillMethod == Image.FillMethod.Vertical)
					{
						if (objImage.fillOrigin == (int)Image.OriginVertical.Bottom && texCorY / texRect.height > objImage.fillAmount) { toExclude.Add(result); continue; }
						if (objImage.fillOrigin == (int)Image.OriginVertical.Top && texCorY / texRect.height < (1 - objImage.fillAmount)) { toExclude.Add(result); continue; }
					}

					#region Radial90
					if (objImage.fillMethod == Image.FillMethod.Radial90)
					{
						if (objImage.fillOrigin == (int)Image.Origin90.BottomLeft)
						{
							if (objImage.fillClockwise && Mathf.Atan(nCorY / nCorX) / (Mathf.PI / 2) < (1 - objImage.fillAmount)) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && Mathf.Atan(nCorY / nCorX) / (Mathf.PI / 2) > objImage.fillAmount) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin90.TopLeft)
						{
							if (objImage.fillClockwise && nCorY < -(1 / Mathf.Tan((1 - objImage.fillAmount) * Mathf.PI / 2)) * nCorX + nHeight) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && nCorY > -(1 / Mathf.Tan(objImage.fillAmount * Mathf.PI / 2)) * nCorX + nHeight) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin90.TopRight)
						{
							if (objImage.fillClockwise && nCorY > Mathf.Tan((1 - objImage.fillAmount) * Mathf.PI / 2) * (nCorX - nWidth) + nHeight) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && nCorY < Mathf.Tan(objImage.fillAmount * Mathf.PI / 2) * (nCorX - nWidth) + nHeight) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin90.BottomRight)
						{
							if (objImage.fillClockwise && nCorY > (1 / Mathf.Tan((1 - objImage.fillAmount) * Mathf.PI / 2)) * (nWidth - nCorX)) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && nCorY < (1 / Mathf.Tan(objImage.fillAmount * Mathf.PI / 2)) * (nWidth - nCorX)) { toExclude.Add(result); continue; }
						}
					}
					#endregion

					#region Radial180
					if (objImage.fillMethod == Image.FillMethod.Radial180)
					{
						if (objImage.fillOrigin == (int)Image.Origin180.Bottom)
						{
							if (objImage.fillClockwise && Mathf.Atan2(nCorY, 2 * (nCorX - nWidth / 2)) < (1 - objImage.fillAmount) * Mathf.PI) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && Mathf.Atan2(texCorY, 2 * (nCorX - nWidth / 2)) > objImage.fillAmount * Mathf.PI) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin180.Left)
						{
							if (objImage.fillClockwise && Mathf.Atan2(nCorX, -2 * (nCorY - nHeight / 2)) < (1 - objImage.fillAmount) * Mathf.PI) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && Mathf.Atan2(nCorX, -2 * (nCorY - nHeight / 2)) > objImage.fillAmount * Mathf.PI) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin180.Top)
						{
							if (objImage.fillClockwise && Mathf.Atan2(nHeight - nCorY, -2 * (nCorX - nWidth / 2)) < (1 - objImage.fillAmount) * Mathf.PI) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && Mathf.Atan2(nHeight - nCorY, -2 * (nCorX - nWidth / 2)) > objImage.fillAmount * Mathf.PI) { toExclude.Add(result); continue; }
						}

						if (objImage.fillOrigin == (int)Image.Origin180.Right)
						{
							if (objImage.fillClockwise && Mathf.Atan2(nWidth - nCorX, 2 * (nCorY - nHeight / 2)) < (1 - objImage.fillAmount) * Mathf.PI) { toExclude.Add(result); continue; }
							if (!objImage.fillClockwise && Mathf.Atan2(nWidth - nCorX, 2 * (nCorY - nHeight / 2)) > objImage.fillAmount * Mathf.PI) { toExclude.Add(result); continue; }
						}
					}
					#endregion

					#region Radial360
					if (objImage.fillMethod == Image.FillMethod.Radial360)
					{
						if (objImage.fillOrigin == (int)Image.Origin360.Bottom)
						{
							if (objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) + Mathf.PI / 2;
								var checkAngle = Mathf.PI * 2 * (1 - objImage.fillAmount);
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle < checkAngle) { toExclude.Add(result); continue; }
							}
							if (!objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) + Mathf.PI / 2;
								var checkAngle = Mathf.PI * 2 * objImage.fillAmount;
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle > checkAngle) { toExclude.Add(result); continue; }
							}
						}

						if (objImage.fillOrigin == (int)Image.Origin360.Right)
						{
							if (objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2);
								var checkAngle = Mathf.PI * 2 * (1 - objImage.fillAmount);
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle < checkAngle) { toExclude.Add(result); continue; }
							}
							if (!objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2);
								var checkAngle = Mathf.PI * 2 * objImage.fillAmount;
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle > checkAngle) { toExclude.Add(result); continue; }
							}
						}

						if (objImage.fillOrigin == (int)Image.Origin360.Top)
						{
							if (objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) - Mathf.PI / 2;
								var checkAngle = Mathf.PI * 2 * (1 - objImage.fillAmount);
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle < checkAngle) { toExclude.Add(result); continue; }
							}
							if (!objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) - Mathf.PI / 2;
								var checkAngle = Mathf.PI * 2 * objImage.fillAmount;
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle > checkAngle) { toExclude.Add(result); continue; }
							}
						}

						if (objImage.fillOrigin == (int)Image.Origin360.Left)
						{
							if (objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) - Mathf.PI;
								var checkAngle = Mathf.PI * 2 * (1 - objImage.fillAmount);
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle < checkAngle) { toExclude.Add(result); continue; }
							}
							if (!objImage.fillClockwise)
							{
								var angle = Mathf.Atan2(nCorY - nHeight / 2, nCorX - nWidth / 2) - Mathf.PI;
								var checkAngle = Mathf.PI * 2 * objImage.fillAmount;
								angle = angle < 0 ? Mathf.PI * 2 + angle : angle;
								if (angle > checkAngle) { toExclude.Add(result); continue; }
							}
						}
					}
					#endregion

				}
				#endregion

				// getting targeted pixel from object's texture and evaluating its alpha
				float alpha = objTex.GetPixel((int)(texCorX + texRect.x), (int)(texCorY + texRect.y)).a;

				// deciding if we need to exclude the object from results list
				if (objAlphaCheck)
				{
					if (objAlphaCheck.IncludeMaterialAlpha) alpha *= objImage.color.a;
					if (alpha < objAlphaCheck.AlphaThreshold) toExclude.Add(result);
				}
				else
				{
					if (IncludeMaterialAlpha) alpha *= objImage.color.a;
					if (alpha < AlphaThreshold) toExclude.Add(result);
				}
			}
			catch (UnityException e)
			{
				if (Application.isEditor && ShowTextureWarnings)
					Debug.LogWarning(string.Format("Check for alpha failed: {0}", e.Message));
			};
		}

		resultAppendList.RemoveAll(r => toExclude.Contains(r));
	}
}
