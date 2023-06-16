Friend Class Item
    Inherits ItemDataClient
    Implements IItem
    Sub New(data As WorldData, itemId As Integer)
        MyBase.New(data, itemId)
    End Sub
End Class
