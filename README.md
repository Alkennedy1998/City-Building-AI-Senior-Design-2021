
# Urban Planning Optimization via “Cities: Skylines” 
Andrew Wang - Jack Cunningham- Carter Duncan - Alexander Kennedy - Advisor Ying Liu

# **Compiling the Cities Skylines Mod**
While the Cities: Skylines game has its own compiler for C# code, we used the Visual Studio method to manually compile and move the resulting file into the appropriate file location. 

To replicate this, we followed the instructions for method 2 in <https://community.simtropolis.com/forums/topic/73404-modding-tutorial-0-your-first-mod/>

The following instructions are from the aforementioned forum post by “boformer”
## Set up the project
Install Visual Studio Community with the .NET desktop development feature, with .NET Framework 3.5 development tools

Create a new project with Select File → New → Project

In the templates section, select Templates → Visual C# → Windows Classic Desktop

Select “Class Library (.NET Framework)”

Select “.NET Framework 3.5” in the dropdown menu

Enter the solution name and local storage location
## Set up the dependencies
Install Cities: Skylines through Steam

In the solution explorer, click “references” then “add reference”

Use the browse button and navigate to the folder Steam\steamapps\common\Cities\_skylines\Cities\_data\Managed

Select the following files:

`	`Assembly-CSharp.dll

`	`ColossalManaged.dll

`	`ICities.dll

`	`UnityEngine.dll

Add the files
## Set up the post build script
Right click the project in the solution explorer and select Properties

Select build events

Copy and paste the following script:

mkdir "%LOCALAPPDATA%\Colossal Order\Cities\_Skylines\Addons\Mods\$(SolutionName)"

del "%LOCALAPPDATA%\Colossal Order\Cities\_Skylines\Addons\Mods\$(SolutionName)\$(TargetFileName)"

xcopy /y "$(TargetPath)" "%LOCALAPPDATA%\Colossal Order\Cities\_Skylines\Addons\Mods\$(SolutionName)"

With Visual Studio now set up with the dependencies and post build script, the C# code for the mod can now be compiled directly into the game files by clicking “rebuild solution” in Visual Studio under the “build” tab.
# **Preparing the Cities: Skylines Game Environment**
The mod and agent development so far has operated on a limited version of the Cities: Skylines mod. Some complexities of the game environment have been removed in order for us to be able to create a minimum viable product given a limited time period and computing power. The limitations are within the game environment and the actions that the agent can take. In order for the agent to run properly, the game environment must have the following mods installed.
## Remove Power Lines
To simplify the game environment, we decided to remove the infrastructure requirement of having power lines connect electricity generators to electricity consumers. In order to change the game environment to this, we used a publically available Cities: Skylines mod available through the steam workshop. Go to following link: <https://steamcommunity.com/sharedfiles/filedetails/?id=572888650>

and click the “subscribe” button. The mod should download automatically. Open Cities: Skylines, in the main menu, click the content manager. Select the mods tab on the left, and make sure the relevant mods are enabled. 
## Remove Pipes
We also decided to remove the infrastructure requirement of having pipes connect water and sewage systems. Repeat the mod installation steps detailed in the Remove Power Lines section, but with the link: <https://steamcommunity.com/sharedfiles/filedetails/?id=576997275>
## Install the Map
Download the TEST\_MAP\_1.crp file

Move the file into C:/Users/[your user]/AppData/Local/ColosalOrder/Cities\_Skylines/Saves

If there is no such directory, launch the Cities: Skylines game first
## Install Python Dependencies
After installing the latest version of Python 3, use pip to install the following modules:

pip install imageio

pip install tensorflow

pip install tf\_agents
# **Launching the Mod and the Agent**
## Launching the Agent
First, the python script must be run before the Cities: Skylines game. 

Navigate to the directory containing the file 

“cities\_skylines\_deepq\_learning\_for\_final\_santa\_clara\_engineering\_project.py”

Open a windows console the same directory

Enter the following command:

`	`python “cities\_skylines\_deepq\_learning\_for\_final\_santa\_clara\_engineering\_project.py”

The agent should now be launched and awaiting the Cities: Skylines side
## Launching the Mod
Launch Steam

Launch Cities: Skylines through the Steam library

Click the load option, and select the test map

After a delay of approximately 30 seconds, observable changes should occur in-game

`	`It is recommended to select the zoning tool in-game to observe zones being assigned
# **Troubleshooting**
## Cities: Skylines Crashes After 30 Seconds
There is a known issue with the reset and quickload functionality based on hardware. If this error is occurring on your machine, navigate to the configuration file, “skylinesconfig.txt”, that should be in the same directory as the “cities\_skylines\_deepq\_learning\_for\_final\_santa\_clara

\_engineering\_project.py” python script. Set the variable “resetting\_allowed” equal to “False” in order to disable this optional feature. This removes some functionality from the learning process but allows the agent to run without crashes. 
# **Cities Skylines Mod Public Classes and Functions**
## Performance Measures
The performance measures class contains only public variables where various gamestate values are stored.

*void get\_performance\_measures()*

This function calls all the relevant API functions and stores the return values in the public member variables.

*void print\_performance\_measures()*

This function creates a single string with all public member variables and labels them. The string is printed to the in-game developer console (F7 to view).

*string performance\_measures\_cs()*

This function creates a single string with all public member variables separated by commas. The string is returned.
## Action Parser
The action parser class contains functions for calling the corresponding API functions based on an input string. The public member variables are meant for debugging and easy format changes for delimiter characters, which are the characters separating tokens. The token\_delimiter char is set to the symbol separating tokens in an input string. The vector\_delimiter char is set to the symbol separating values in a vector. When debug is set to true, related prints are sent to the in-game developer console.

*void parse\_actions(string)*

This function takes the input string as a parameter. It then calls the function split\_string\_to\_tokens and passes it the input string as well as a delimiter symbol, defined as a member variable. split\_string\_to\_tokens returns a list of strings, which is then passed to the function run\_tokens. The string should be formatted with the name of the action as the first token, followed by the values of the parameters. The parameters must be able to be parsed from string representation to the expected value type. Each token should be separated by the token\_delimiter character (currently set to ‘|’). In the case of a vector being passed as a parameter, each value should be separated by the vector\_delimiter char (currently set to ‘,’). 

*list<string> split\_string\_to\_tokens(string, char)*

This function takes an input string and delimiter character as parameters. The string will be split into substrings between each delimiter character. The substrings are then stored in a list of strings which is returned after all tokens have been read. 

*vector3 string\_to\_vector3(string)*

This function takes an input string as a parameter. The string will be split into substrings, similarly to the split\_string\_to\_tokens function, but in this case, the substrings will be converted to floats. The corresponding x, y, and z values are formatted into a vector3 and returned.

*void run\_tokens(List)*

This function takes a list of strings as a parameter. The first token will be compared in a switch statement to set actions. Currently, the possible first token values are the either the string “createbuilding” or  the string “createzone”. If the first token does not match either, no action will occur. Once the corresponding switch body has been entered, the expected parameters will be parsed from the second token onwards. If the token’s string value is incompatible with the appropriate value type, the game may crash or the mod may restart due to the exception. Once the arguments are set to the appropriate types, the API function will be called with the arguments, executing the action in-game. 

DataReader

The DataReader class is the core of the mod, and extends MonoBehavior, which allows Unity to run the class alongside the rest of the game code during runtime. This class contains private functions Start() and Update() which are executed by Unity’s mod interface automatically while Cities: Skylines is running. The DataReader class contains the main action classes which allow selected actions to be executed in game, as well as the threaded function in charge of sending and receiving data from the python agent, and a function to reload the game from the most recent save in order to “reset” the agent between episodes. 

`	`*bool CreateBuilding(out ushort, uint, Vector3, float, int)*

This function takes 5 arguments. The first is a ushort passed as an out variable, which will be set to the ID of the building when it is constructed, or 0 if it was unable to construct the building. The remaining 4 are the building’s prefab ID, position, angle, and hitbox length. This function fetches the prefab, checks for collision errors, then places the specified building using a public function of the class that handles construction within Cities: Skylines. The return value is true if the construction was successful, or false if there were collision errors. 

*int ZoneArea(Vector3, int, quantity)*

This function takes 3 arguments: The position of the zoning as a Vector3, the type of zone to be placed as an int, and the quantity of zones to be zoned in the area. This function determines the closest zonable tile to the specified location and zones it with the specified zone using a public function of the class that handles zoning with Cities: Skylines. This process is repeated until the specified quantity of zones have been zoned, or when no more zones are found within 5000 pixels of the specified location. The return value is the quantity of zones that were zoned. 

*void sendData()*

This function starts the thread which reads and writes to the named pipes that communicate with the Python element of the system. This thread is only created once and operates through resets of the game. 

*void reset()*

This function loads the game to the most recently saved save file using a public function of the class that handles loading within Cities: Skylines. This allows the agent to reset the state of the game to the beginning between iterations. 

# **Python - deepQ learning TensorFlow Script**
### Game Data Capture
The first step is to acquire data the agent will learn from. This information, such as population count, income, and available power, is aggregated using the previously discussed C# script and sent to the python application through a data pipe and saved in Python objects.
### Game State Analysis
` `Using the game state information acquired in the previous step, the AI agent will have to make a decision about which action to take. At the start of training the agent’s actions are seemingly random. As the agent begins taking actions, each action will result in a new game state with each metric being affected positively or negatively. As the agent makes these choices it learns the benefits and drawbacks of the resulting game state from each ation. This results in the agent slowly realizing that certain actions will produce more desirable game states, reflected via higher scoring results. 
### State-to-Goal Calculation

After an action is taken by the agent it will be given a reward score depending on how well it aligned with a set of criteria the user defined. As we chose to use a Deep-Q Learning network architecture, for each possible action the agent can take, the agent calculates an expected reward for each action and resulting state pairing. Then the agent chooses the action with the highest reward. If the user wants to ensure the city has an ample supply of power and the agent took actions that resulted in the power supply being overtaken by power demand, the reward score given to the agent would be lower.
### Action Selection 
Using the scores discussed above, the agent will choose the action with the highest score. This action will then be encoded and sent over a system pipe to the C# script where the action will be decoded and the corresponding action will be taken in the Cities: Skylines game. For example, one possible selected action could be to build a hospital in square (2,7). This information would be encoded as an integer such as “96024” and sent over as discussed. 

class CitiesSkylinesEnvironment(py\_environment.PyEnvironment):

The environment parameter is the class defined to interact with the Cities:Skylines C# API

def compute\_avg\_return(environment, policy, num\_episodes=10):


The policy parameter is the predefined random\_tf\_policy that selects actions in a random way.

random\_tf\_policy.RandomTFPolicy(train\_env.time\_step\_spec(), train\_env.action\_spec())

The higher the num\_episodes parameter the more iterations the program will run

We decided to use a deep Q-network to train with. The DQN agent is defined as follows:

    agent = dqn\_agent.DqnAgent(

    tf\_env.time\_step\_spec(),

    tf\_env.action\_spec(),

    q\_network=q\_net,

    optimizer=optimizer,

    td\_errors\_loss\_fn=common.element\_wise\_squared\_loss,

    train\_step\_counter=train\_step\_counter)
