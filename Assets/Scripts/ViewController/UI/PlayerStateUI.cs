using System;
using System.Collections;
using System.Collections.Generic;
using Framework;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace Elvenwood
{
	public class PlayerStateUI : AbstractController
	{
		[Header("生命值")]
		public Sprite healthHeart;
		public Sprite emptyHeart;

		public GameObject healthBar;

		public Image heartImg;

		private int maxHealth;
		private List<Image> healthImages = new List<Image>();

		[Tooltip("爆发技能")]
		public Slider switchSkillSlider;

		[Header("Boss钥匙")] public Image keyImg;

		public List<Sprite> keyImgs = new List<Sprite>();

		[Header("技能图片")]
		public Image magicBowImg;

		public Image cloneImg;


		private void Start()
		{
			this.RegisterEvent<OnUpdateUI>(e =>
			{

				UpdateHealth(this.SendQuery(new PlayerCurrentHpQuery()));
				UpdateKeyValue();
				UpdateSwitchValue(0);
			});

			this.RegisterEvent<OnGetSkill>(e =>
			{
				UpdateSkillIcon();
			});
			//获取最大生命值
			//maxHealth = this.SendQuery(new PlayerCurrentHpQuery());
			maxHealth = this.GetModel<IPlayerModel>().Data.maxHp.Value;


			//maxHealth = 9;

			for (int i = 0; i < maxHealth; i++)
			{
				//创建Image，设置父子关系
				Image newHeart = Instantiate(heartImg);
				newHeart.transform.SetParent(healthBar.transform);
				//设置初始图片
				newHeart.sprite = healthHeart;
				//设置位置
				newHeart.rectTransform.anchoredPosition = new Vector2(i * heartImg.rectTransform.rect.width, 0f);
				newHeart.gameObject.SetActive(true);
				healthImages.Add(newHeart);
			}

			//初始化数据
			UpdateHealth(this.SendQuery(new PlayerCurrentHpQuery()));
			UpdateSwitchValue(0);
			UpdateKeyValue();

			//UpdateHealth(3);


		}

		public void UpdateHealth(int health)
		{
			int newmaxHealth = this.GetModel<IPlayerModel>().Data.maxHp.Value;

			if (newmaxHealth != maxHealth)
			{

				Image newHeart = Instantiate(heartImg);
				newHeart.transform.SetParent(healthBar.transform);
				//设置初始图片
				newHeart.sprite = healthHeart;
				//设置位置
				newHeart.rectTransform.anchoredPosition = new Vector2(maxHealth * heartImg.rectTransform.rect.width, 0f);
				newHeart.gameObject.SetActive(true);
				healthImages.Add(newHeart);
				maxHealth = newmaxHealth;
			}

			if (health > maxHealth)
			{
				health = maxHealth;
			}
			for (int i = 0; i < health; i++)
			{
				healthImages[i].sprite = healthHeart;
				//更改透明度
				Color tmpColor = healthImages[i].color;
				tmpColor.a = 1f;
				healthImages[i].color = tmpColor;
			}

			for (int i = health; i < maxHealth; i++)
			{
				healthImages[i].sprite = emptyHeart;
				//更改透明度
				Color tmpColor = healthImages[i].color;
				tmpColor.a = 0.8f;
				healthImages[i].color = tmpColor;
			}
		}

		public void UpdateSwitchValue(float newValue)
		{
			switchSkillSlider.value = newValue;
		}

		public void UpdateKeyValue()
		{
			keyImg.sprite = keyImgs[this.SendQuery(new CurrentKeyCount())];
		}

		public void UpdateSkillIcon()
		{
			PlayerSkill tmpPlayerSkill = this.SendQuery(new PlayerSkillState());
			Debug.Log("角色技能信息:" + tmpPlayerSkill);

			if (tmpPlayerSkill.hasMagicArrow)
			{
				magicBowImg.gameObject.SetActive(true);
			}

			if (tmpPlayerSkill.hasCloneSkill)
			{
				cloneImg.gameObject.SetActive(true);
			}
		}

		// public void OnGetMagicBow()
		// {
		//     magicBowImg.gameObject.SetActive(true);
		// }
		//
		// public void OnGetClone()
		// {
		//     cloneImg.gameObject.SetActive(true);
		// }


	}


}

