using System;

namespace ZuneUI;

[Flags]
public enum BlockListUpdatesReason
{
	None = 0,
	ModalUI = 1,
	JumpInList = 2,
	DragSelect = 4,
	DragDrop = 8,
	Edit = 0x10,
	Focus = 0x20
}
