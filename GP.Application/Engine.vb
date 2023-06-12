Public Class Engine
    Inherits BasePresenter
    Implements IEngine
    Private ReadOnly _config As GPConfig
    Sub New(config As GPConfig)
        _config = config
    End Sub
    Public Sub Update(commands As IEnumerable(Of String), ticks As Long) Implements IEngine.Update

    End Sub
End Class
