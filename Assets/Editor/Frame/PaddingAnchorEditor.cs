using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PaddingAnchor), true)]
public class PaddingAnchorEditor : GameEditorBase
{
	private PaddingAnchor anchor;
	public override void OnInspectorGUI()
	{
		serializedObject.Update();
		anchor = target as PaddingAnchor;
		anchor.mAdjustFont = displayToggle("AdjustFont", anchor.mAdjustFont);
		// ������EditorGUILayout.EnumPopup���ַ�ʽ��ʾ���������ڿ����ڱ༭���޸�,���Ҵ���ָ���߼�
		var anchorMode = (ANCHOR_MODE)displayEnum("AnchorMode", anchor.mAnchorMode);
		if (anchorMode != anchor.mAnchorMode)
		{
			anchor.setAnchorModeInEditor(anchorMode);
		}
		var relativePos = displayToggle("RelativePosition", anchor.mRelativeDistance);
		if (relativePos != anchor.mRelativeDistance)
		{
			anchor.setRelativeDistanceInEditor(relativePos);
		}
		// ֻ��ͣ�������ڵ��ĳ��λ��
		if (anchor.mAnchorMode == ANCHOR_MODE.AM_PADDING_PARENT_SIDE)
		{
			var horizontalPadding = (HORIZONTAL_PADDING_SIDE)displayEnum("HorizontalPaddingSide", anchor.mHorizontalNearSide);
			if (horizontalPadding != anchor.mHorizontalNearSide)
			{
				anchor.setHorizontalNearSideInEditor(horizontalPadding);
			}
			var verticalPadding = (VERTICAL_PADDING_SIDE)displayEnum("VerticalPaddingSide", anchor.mVerticalNearSide);
			if (verticalPadding != anchor.mVerticalNearSide)
			{
				anchor.setVerticalNearSideInEditor(verticalPadding);
			}
			// HPS_CENTERģʽ�²Ż���ʾmHorizontalPosition
			if (anchor.mHorizontalNearSide == HORIZONTAL_PADDING_SIDE.HPS_CENTER)
			{
				// displayProperty����ֻ�Ǽ򵥵�ʹ��Ĭ�Ϸ�ʽ��ʾ����,���ڱ༭����ֱ���޸�ֵ,���ܴ����κ������߼�
				displayProperty("mHorizontalPositionRelative", "HorizontalPositionRelative");
				displayProperty("mHorizontalPositionAbsolute", "HorizontalPositionAbsolute");
			}
			// VPS_CENTERģʽ�²Ż���ʾmVerticalPosition
			if (anchor.mVerticalNearSide == VERTICAL_PADDING_SIDE.VPS_CENTER)
			{
				displayProperty("mVerticalPositionRelative", "VerticalPositionRelative");
				displayProperty("mVerticalPositionAbsolute", "VerticalPositionAbsolute");
			}
			// ��ʾ�߾������
			if (anchor.mHorizontalNearSide != HORIZONTAL_PADDING_SIDE.HPS_CENTER ||
				anchor.mVerticalNearSide != VERTICAL_PADDING_SIDE.VPS_CENTER)
			{
				displayProperty("mDistanceToBoard", "DistanceToBoard");
			}
		}
		else
		{
			displayProperty("mAnchorPoint", "AnchorPoint");
		}
		serializedObject.ApplyModifiedProperties();
	}
}