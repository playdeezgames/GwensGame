Module Status
    Public ShowHouse As Action =
        Sub()
            AddPrompt("Yer at yer house!")
        End Sub
    Sub DefaultStatus()
        AddPrompt($"You are at ({Data.GetX()}, {Data.GetY()}) and facing {GetDirectionName(Data.GetFacing())}.")
    End Sub
    Dim locations As New Dictionary(Of Integer, Dictionary(Of Integer, Action)) From
        {
            {0, New Dictionary(Of Integer, Action) From {
                {0, ShowHouse}
            }}
        }
    Sub ShowSpecificStatus()
        If locations.ContainsKey(GetX()) Then
            If locations(GetX()).ContainsKey(GetY()) Then
                locations(GetX())(GetY())()
                Return
            End If
        End If
        DefaultStatus()
    End Sub
    Public ShowStatus As Action =
    Sub()
        ClearInputItems()
        AddInputItem("Move...", ShowMoveMenu)
        AddInputItem("Turn...", ShowTurnMenu)
        ShowSpecificStatus()
        AddInputItem("Main Menu", ShowMainMenu)
    End Sub
End Module
