Public Class WorldDataClient
    Private data As WorldData
    Protected ReadOnly Property WorldData As WorldData
        Get
            Return data
        End Get
    End Property
    Protected Sub New(data As WorldData)
        Me.data = data
    End Sub
End Class
