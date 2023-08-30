DOTNET_COMMAND = dotnet publish -c Release --self-contained true /p:PublishSingleFile=true
WINDOWS_RID = win10-x64
LINUX_RID = linux-x64
MAC_RID = osx-x64

WINDOWS_EXE = system-toolbox.exe
LINUX_EXE = system-toolbox
MAC_EXE = system-toolbox

WINDOWS_EXE_NEW = win_$(WINDOWS_EXE)
LINUX_EXE_NEW = linux_$(LINUX_EXE)
MAC_EXE_NEW = mac_$(MAC_EXE)

WINDOWS_PATH = bin/Release/net7.0/win10-x64/publish/$(WINDOWS_EXE)
LINUX_PATH = bin/Release/net7.0/linux-x64/publish/$(LINUX_EXE)
MAC_PATH = bin/Release/net7.0/osx-x64/publish/$(MAC_EXE)

default:
	@echo 'Targets:'
	@echo '  copy-all'
	@echo '  build-all'
	@echo '  build-win'
	@echo '  build-linux'
	@echo '  build-mac'
	@echo '  clean'

copy-all: build-all
	cp $(WINDOWS_PATH) $(WINDOWS_EXE_NEW)
	cp $(LINUX_PATH) $(LINUX_EXE_NEW)
	cp $(MAC_PATH) $(MAC_EXE_NEW)

build-all: build-win build-linux build-mac

build-win:
	$(DOTNET_COMMAND) -r $(WINDOWS_RID)

build-linux:
	$(DOTNET_COMMAND) -r $(LINUX_RID)

build-mac:
	$(DOTNET_COMMAND) -r $(MAC_RID)

clean:
	-rm -rf bin/
	-rm -rf obj/
	-rm -f $(WINDOWS_EXE_NEW)
	-rm -f $(LINUX_EXE_NEW)
	-rm -f $(MAC_EXE_NEW)
