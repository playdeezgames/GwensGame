Friend Class NeutralState
    Inherits BaseState
    Public Sub New(stateMachine As IApplicationStateMachine, frameBuffer As IFrameBuffer)
        MyBase.New(stateMachine, frameBuffer)
    End Sub
    Private choiceTable As New Dictionary(Of Integer, (String, Action))
    Private choiceIndex As Integer
    Public Overrides Sub Run()
        Dim avatar = Engine.World.Avatar
        If avatar.HasWon Then
            GoToState(GameStates.Win)
            Return
        End If
        If avatar.IsDead Then
            Engine.PlaySfx(Sfx.PlayerDead)
            GoToState(GameStates.Dead)
            Return
        End If
        choiceTable.Clear()
        Dim choiceIndex = 1

        choiceTable(0) = ("Game Menu", AddressOf HandleGameMenu)

        choiceTable(choiceIndex) = ("Keep Going!", AddressOf HandleKeepGoing)
        choiceIndex += 1

        choiceTable(choiceIndex) = ("Change Pace", AddressOf HandleChangePace)
        choiceIndex += 1

        If avatar.Snax > 0 Then
            choiceTable(choiceIndex) = ("Eat Snax", AddressOf HandleEatSnax)
            choiceIndex += 1
        End If

        If avatar.CanForage Then
            choiceTable(choiceIndex) = ("Forage", AddressOf HandleForage)
            choiceIndex += 1
        End If
        With FrameBuffer
            .WriteLine(, Brown, Black)
            .WriteLine("Current Status:")
            .WriteLine($"You have {avatar.DistanceRemaining} miles left to go!", Gray)

            .Write($"Yer health: ", Gray)
            .WriteLine($"{avatar.Health}/{avatar.MaximumHealth}", GetStatisticColor(avatar.Health, avatar.MaximumHealth))

            .Write($"Yer satiety: ", Gray)
            .WriteLine($"{avatar.Satiety}/{avatar.MaximumSatiety}", GetStatisticColor(avatar.Satiety, avatar.MaximumSatiety))

            .Write($"Yer pace: ", Gray)
            .WriteLine(GetPaceName(avatar.Pace), GetPaceColor(avatar.Pace))

            If avatar.CanForage Then
                .Write("Foraging: ", Gray)
                .WriteLine(GetForageDifficultyName(avatar.ForagingDifficulty), GetForageDifficultyColor(avatar.ForagingDifficulty))
            End If

            If avatar.Snax > 0 Then
                .WriteLine($"You have {avatar.Snax} snax.", Gray)
            End If

            For index = 1 To choiceIndex - 1
                .WriteLine($"{index}. {choiceTable(index).Item1}", Gray)
            Next
            .WriteLine($"0. {choiceTable(0).Item1}", Gray)
        End With
        ShowPrompt()
    End Sub

    Private Sub HandleForage()
        GoToState(Forage)
    End Sub

    Private Sub HandleEatSnax()
        GoToState(GameStates.EatSnax)
    End Sub

    Private Sub HandleChangePace()
        GoToState(GameStates.ChangePace)
    End Sub

    Private Sub HandleKeepGoing()
        GoToState(GameStates.KeepGoing)
    End Sub

    Private Sub HandleGameMenu()
        GoToState(GameStates.GameMenu)
    End Sub

    Private Function GetStatisticColor(value As Integer, maximum As Integer) As Integer
        Select Case CInt(Math.Ceiling((value / maximum) * 4.0))
            Case Is >= 4
                Return Green
            Case 3
                Return Yellow
            Case 2
                Return Brown
            Case Else
                Return Red
        End Select
    End Function

    Protected Overrides Function HandleChoice(choice As Integer) As Boolean
        If choiceTable.ContainsKey(choice) Then
            choiceTable(choice).Item2()
            Return True
        End If
        Return False
    End Function
End Class
