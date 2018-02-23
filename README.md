# MasterServerUDP
UDP version of the Forge Networking Master Server.

The next phase in building my Unity3D game is adding networking functionality. After trying out a few networking assets I've set my sights on Forge Networking. It's an open source networking solution for Unity3D with an active community on Discord. Forge includes a MasterServer (which keeps track of the game hosts) and NATHolePunch which allows you to run a server behind a (NAT) router. Unfortunately I haven't been able to get the provided MasterServer (based on TCP connections) to work on an Azure VM. So I decided to rewrite it into an UDP version.

This repository contains:
* C# files used to build the UDP version of the MasterServer.
* A NetworkManager.cs files which extends that class (using a partial class) to include the UDP alternatives.

## Important to know:

* Read through the Forge documentation and run the samples first. Get to know Forge Networking first.
* For this to work you'll need to have some basic knowledge of Visual Studio (you can use the community edition).
* You need some basic knowledge of TCP networking.

# Installation:

* Download and install Forge Networking [1].
* Setup the Forge Networking kit in Visual Studio [2].
* Download this repo and add the Visual Studio files as a project to the BeardedManStudios solution. Check if you need to update any project references.
* Build the MasterServerUDP solution.

# Usage:

* Copy the MasterServerUDP files to you server (in my case an Azure VM).
* Run the MasterServerUDP.exe.
* Add the files from this repo's Unity folder to your project.
* Update you MainMenu (or other custom) by replacing the Initialize function with InitializeUDP and RegisterOnMasterServer with RegisterOnMasterServerUDP.

(I'll add a video on how to setup an Azure VM soon)

# Limitations:

This repository does not / will not contain the executable version of the MasterServerUDP. Review the code and build it yourself to make sure there are no security risks.

# License:

MasterServerUDP is freely available under the MIT license. If you make any improvements, then please submit a pull request with them so everyone else can share in the knowledge.

# References:

[1] Forge Networking: https://github.com/BeardedManStudios/ForgeNetworkingRemastered

[2] Installing Forge Networking in Visual Studio (by Kirk Stallings): https://youtu.be/Rtx7dCtspV4



