Public Interface IForaging
    Sub GenerateDifficulty()
    Sub GenerateAbundance()
    Sub Reset()
    Function DifficultyName() As String
    Function CanForage() As Boolean
End Interface
