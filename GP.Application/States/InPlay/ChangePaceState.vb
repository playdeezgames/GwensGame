Friend Class ChangePaceState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        With _frameBuffer
            .BackgroundColor = Black
            .WriteLine()

            .ForegroundColor = Gray
            .Write("Current Pace: ")
            .ForegroundColor = GetPaceColor(avatar.Pace)
            .WriteLine(GetPaceName(avatar.Pace))

            .ForegroundColor = Brown
            .WriteLine("Change Pace:")
            For paceIndex = MinimumPace To MaximumPace
                .ForegroundColor = Gray
                .Write($"{paceIndex}. ")
                .ForegroundColor = GetPaceColor(paceIndex)
                .WriteLine(GetPaceName(paceIndex))
            Next
            .ForegroundColor = Gray
            .WriteLine("0. Go Back")
        End With
        ShowPrompt()
    End Sub
    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        Select Case choice
            Case 0
                GoToState(Neutral)
            Case MinimumPace To MaximumPace
                Engine.World.Avatar.SetPace(choice)
                GoToState(Neutral)
            Case Else
                Return False
        End Select
        Return True
    End Function
End Class
