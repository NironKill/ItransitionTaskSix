Implement a "collaborative presentation software" for everyone. 

No registration or authentication, all users have immediate access to the "presentations" list (the user only provides arbitrary nick-name to enter).

Each user can create presentation or join existing presentation. Presentation creator can see the list of the connected users (let's say on the right) and can make any other users "Editors" (or switch them back to "Viewers").

Viewer can only view the collaborative work (no editing tools).

Several editors can edit the same or different slides 
(only creator can add new slides or remove them). When somebody edit something, it appears to other users "immediately" (there may be a slight delay, you can either poll the server or preferably use websockets). 

Everything edited on the slides is stored "forever" (if user joins this presentation later, he/she sees everything what was created before).

Slide area should fill the whole window (except the top tool panel, left slides panel and right users panel) and scale/scroll adequately.

At least, add possiblity to add editable, movable text blocks with markdown formatting.

Of course, in the presentation software there should be "present" mode.
 
