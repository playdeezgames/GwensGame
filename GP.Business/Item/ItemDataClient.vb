Friend Class ItemDataClient
    Inherits WorldDataClient
    Protected Sub New(data As WorldData, itemId As Integer)
        MyBase.New(data)
        Me.ItemId = itemId
    End Sub
    Protected Property ItemId As Integer
    Protected ReadOnly Property ItemData As ItemData
        Get
            Return WorldData.Items(ItemId)
        End Get
    End Property
End Class
