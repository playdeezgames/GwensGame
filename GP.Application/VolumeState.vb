Friend Class VolumeState
    Inherits BaseState
    Private ReadOnly _setVolume As Action(Of Single)
    Private ReadOnly _getVolume As Func(Of Single)
    Public Sub New(
                  stateMachine As IApplicationStateMachine,
                  frameBuffer As IFrameBuffer,
                  setVolume As Action(Of Single),
                  getVolume As Func(Of Single))
        MyBase.New(stateMachine, frameBuffer)
        _setVolume = setVolume
        _getVolume = getVolume
    End Sub
    Public Overrides Sub Start()
        With _frameBuffer
            .ForegroundColor = Gray
            .BackgroundColor = Black
            .WriteLine()
            .WriteLine($"Volume(currently {_getVolume() * 100.0F:f0}%):")
            For n = 0 To 10
                .WriteLine($"{n + 1}. {n * 10}%")
            Next
            .WriteLine("0. Go Back")
            .WriteLine()
            .Write("> ")
        End With
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                ChangeToState(Options)
            Case 1 To 11
                _setVolume((choice - 1) / 10.0F)
                Start()
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
