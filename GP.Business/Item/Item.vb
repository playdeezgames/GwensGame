Friend Class Item
    Inherits ItemDataClient
    Implements IItem
    Sub New(data As WorldData, itemId As Integer)
        MyBase.New(data, itemId)
    End Sub
    Public ReadOnly Property Id As Integer Implements IItem.Id
        Get
            Return ItemId
        End Get
    End Property

    Public ReadOnly Property ItemType As String Implements IItem.ItemType
        Get
            Return ItemData.ItemType
        End Get
    End Property

    Public Sub Destroy() Implements IItem.Destroy
        ItemData.Destroyed = True
    End Sub
End Class
