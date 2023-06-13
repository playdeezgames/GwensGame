Public MustInherit Class BaseState
    Implements IApplicationState
    Protected ReadOnly _stateMachine As IApplicationStateMachine
    Protected ReadOnly _frameBuffer As IFrameBuffer
    Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        _stateMachine = stateMachine
        _frameBuffer = frameBuffer
    End Sub
    Public MustOverride Sub Handle(tokens As IEnumerable(Of String)) Implements IApplicationState.Handle
    Public MustOverride Sub Start() Implements IApplicationState.Start
    Protected Sub ShowError(message As String)
        _frameBuffer.ForegroundColor = Red
        _frameBuffer.BackgroundColor = Black
        _frameBuffer.WriteLine()
        _frameBuffer.WriteLine(message)
        Start()
    End Sub
End Class
