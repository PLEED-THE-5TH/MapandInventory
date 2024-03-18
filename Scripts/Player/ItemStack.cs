using Godot;
using System;

public class ItemStack
{
    public Item item { get; private set; }
    public int size { get; private set; }
    public event EventHandler Changed;
    public enum ConflictAction
    {
        HALT,
        REPLACE
    }
    public ItemStack(Item initItem = null, int initSize = 1)
    {
        if (initItem is null) {
            return;
        }
        BaseSetItemAndSize(initItem, initSize);
    }
    public ItemStack(ItemStack itemStack) : this(itemStack.item, itemStack.size) { }
    public bool SetItem(Item newItem, ConflictAction conflictAction = ConflictAction.REPLACE)
    {
        if (!BaseSetItem(newItem, conflictAction)) {
            return false;
        }
        Changed.Invoke(this, new());
        return true;
    }
    public bool SetSize(int newSize)
    {
        if (!BaseSetSize(newSize)) {
            return false;
        }
        Changed.Invoke(this, new());
        return true;
    }
    public bool SetItemAndSize(Item newItem, int newSize, ConflictAction conflictAction = ConflictAction.REPLACE)
    {
        if (!BaseSetItemAndSize(newItem, newSize, conflictAction)) {
            return false;
        }
        Changed.Invoke(this, new());
        return true;
    }
    public bool SetStack(ItemStack itemStack, ConflictAction conflictAction = ConflictAction.REPLACE)
    {
        return SetItemAndSize(itemStack.item, itemStack.size, conflictAction);
    }
    public bool IncrementSize(int count)
    {
        return SetSize(size + count);
    }
    public bool Combine(ItemStack itemStack, bool setIfEmpty = false)
    {
        if (IsEmpty() && setIfEmpty) {
            if (!SetStack(itemStack)) {
                return false;
            }
            itemStack.Empty();
            return true;
        }
        if (size == item.maxStackSize) {
            return false;
        }
        if (ItemsConflict(itemStack)) {
            return false;
        }
        int itemsAdded = Mathf.Min(itemStack.size, item.maxStackSize - size);
        if (!IncrementSize(itemsAdded)) {
            return false;
        }
        if (!IncrementSize(itemsAdded)) {
            return false;
        }
        if (!itemStack.IncrementSize(-itemsAdded)) {
            size -= itemsAdded;
            return false;
        }
        return true;
    }
    public bool ItemsConflict(ItemStack itemStack)
    {
        return ItemStack.Conflict(item, itemStack.item);

    }
    static public bool Conflict(Item item1, Item item2)
    {
        if (item1 == item2) {
            return item1.name != item2.name;
        }
        else {
            return (item1 != null) != (item2 != null);
        }
    }
    public bool IsEmpty()
    {
        return size == 0 || item is null;

    }
    public void Empty()
    {
        BaseEmpty();
        Changed.Invoke(this, new());
    }
    public ItemStackDrop Drop()
    {
        if (IsEmpty()) {
            return null;
        }
        ItemStackDrop stackDrop = ItemStackDropManager.CreateDrop(new ItemStack(this));
        Empty();
        return stackDrop;
    }
    private void BaseEmpty()
    {
        item = null;
        size = 0;
    }
    private bool BaseSetItemAndSize(Item newItem, int newSize, ConflictAction conflictAction = ConflictAction.REPLACE)
    {
        if (newSize == 0) {
            BaseEmpty();
            return true;
        }
        Item oldItem = item;
        if (!BaseSetItem(newItem, conflictAction)) {
            return false;
        }
        if (IsEmpty()) {
            return true;
        }
        if (!BaseSetSize(newSize)) {
            item = oldItem;
            return false;
        }
        return true;

    }
    private bool BaseSetItem(Item newItem, ConflictAction conflictAction = ConflictAction.REPLACE)
    {
        void AssignItem()
        {
            item = newItem;
            size = item is not null ? 1 : 0;
        }
        if (ItemStack.Conflict(item, newItem)) {
            switch (conflictAction) {
                case ConflictAction.HALT:
                    return false;
                case ConflictAction.REPLACE:
                    AssignItem();
                    return true;
            }
            return false;
        }
        AssignItem();
        return true;
    }
    private bool BaseSetSize(int newSize)
    {
        if (newSize == 0) {
            BaseEmpty();
            return true;
        }
        if (IsEmpty()) {
            return false;
        }
        if (newSize < 0) {
            return false;
        }
        if (newSize > item.maxStackSize) {
            return false;
        }
        size = newSize;
        return true;
    }
}