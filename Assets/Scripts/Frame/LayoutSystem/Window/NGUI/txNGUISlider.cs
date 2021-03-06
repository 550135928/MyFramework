﻿using UnityEngine;
using System.Collections;

#if USE_NGUI

public class txNGUISlider : txNGUIObject, ISlider
{	
	protected UISlider mSlider;
	public override void init(GameObject go, txUIObject parent)
	{
		base.init(go, parent);
		mSlider = getUnityComponent<UISlider>();
	}
	public float getValue(){return mSlider.value;}
	public void setValue( float value)
	{
		saturate(ref value);
		mSlider.value = value;
	}
	public void setSliderValueChange(EventDelegate.Callback mUislider) 
	{
		EventDelegate.Add(mSlider.onChange, mUislider);
	}
	public override void setAlpha(float alpha, bool fadeChild) {mSlider.alpha = alpha;}
	public override float getAlpha(){return mSlider.alpha;}
}

#endif