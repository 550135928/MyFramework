﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Command : GameBase
{
	public List<CommandCallback> mEndCallback;	// 命令执行完毕时的回调函数
	public List<CommandCallback> mStartCallback;// 命令开始执行时的回调函数
	public EXECUTE_STATE mExecuteState; // 命令执行状态
	public CommandReceiver mReceiver;   // 命令接受者
	public Type mType;					// 命令类型
	public BOOL mResult;				// 命令的执行结果,只用于部分需要知道执行结果的命令使用
	public string mFile;				// 命令发出代码文件名
	public object mUserData;			// 用作传参,可在命令回调中使用Command.mUserData获取
	public float mDelayTime;			// 命令当前延迟时间
	public bool mShowDebugInfo;			// 是否显示调试信息
	public bool mDelayCommand;			// 是否是延迟执行的命令
	public bool mValid;					// 是否为有效命令
	public bool mIgnoreTimeScale;		// 命令的延迟时间是否不受时间缩放影响
	public int mCmdID;					// 命令ID,每一个命令对象拥有一个唯一ID
	public int mAssignID;				// 重新分配时的ID,每次分配都会设置一个新的唯一执行ID
	public int mLine;					// 命令发出的代码行号
	public Command()
	{
		mEndCallback = new List<CommandCallback>();
		mStartCallback = new List<CommandCallback>();
		mReceiver = null;
		mValid = false;
		mAssignID = -1;
		mResult = null;
		mUserData = null;
	}
	public virtual void init()
	{
		mReceiver = null;
		mUserData = null;
		mResult = null;
		mShowDebugInfo = true;
		mDelayCommand = false;
		mValid = false;
		mIgnoreTimeScale = false;
		mExecuteState = EXECUTE_STATE.ES_NOT_EXECUTE;
		mEndCallback.Clear();
		mStartCallback.Clear();
		mDelayTime = 0.0f;
	}
	// 命令执行
	public virtual void execute() { }
	// 调试信息，由CommandSystem调用
	public virtual string showDebugInfo()			{ return GetType().ToString(); }
	public bool isShowDebugInfo()					{ return mShowDebugInfo; }
	public bool isDelayCommand()					{ return mDelayCommand; }
	public CommandReceiver getReceiver()			{ return mReceiver; }
	public bool isValid()							{ return mValid; }
	public Type getType()							{ return mType; }
	public EXECUTE_STATE getExecuteState()			{ return mExecuteState; }
	public float getDelayTime()						{ return mDelayTime; }
	public bool isIgnoreTimeScale()					{ return mIgnoreTimeScale; }
	public void setShowDebugInfo(bool show)			{ mShowDebugInfo = show; }
	public void setDelayCommand(bool delay)			{ mDelayCommand = delay; }
	public void setReceiver(CommandReceiver Reciver){ mReceiver = Reciver; }
	public void setValid(bool valid)				{ mValid = valid;}
	public void setType(Type type)					{ mType = type; }
	public void setExecuteState(EXECUTE_STATE state){ mExecuteState = state; }
	public void setAssignID(int id)					{ mAssignID = id; }
	public void setID(int id)						{ mCmdID = id; }
	public void setDelayTime(float time)			{ mDelayTime = time; }
	public void setIgnoreTimeScale(bool ignore)		{ mIgnoreTimeScale = ignore; }
	public void addEndCommandCallback(CommandCallback cmdCallback)
	{
		if (cmdCallback != null)
		{
			mEndCallback.Add(cmdCallback);
		}
	}
	public void addStartCommandCallback(CommandCallback cmdCallback)
	{
		if (cmdCallback != null)
		{
			mStartCallback.Add(cmdCallback);
		}
	}
	public void runEndCallBack()
	{
		foreach(var item in mEndCallback)
		{
			item(this);
		}
		mEndCallback.Clear();
	}
	public void runStartCallBack()
	{
		foreach (var item in mStartCallback)
		{
			item(this);
		}
		mStartCallback.Clear();
	}
}