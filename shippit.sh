dotnet publish ./GwensJourney/GwensJourney.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./GwensJourney/GwensJourney.vbproj -o ./pub-windows -c Release --sc -r win-x64
dotnet publish ./GwensJourney/GwensJourney.vbproj -o ./pub-mac -c Release --sc -r osx-x64
butler push pub-windows thegrumpygamedev/gwens-journey:windows
butler push pub-linux thegrumpygamedev/gwens-journey:linux
butler push pub-mac thegrumpygamedev/gwens-journey:mac
git add -A
git commit -m "shipped it!"