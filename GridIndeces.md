# Introduction #

Unfortunately, MikeShe and other MikeZero products do not always agree on a how a grid is to be understood.



# Details #

The first difference is that MikeShe counts from 1 whereas other MikeZero products counts from 0. The MikeSheWrapper counts from 0.

The second difference is that MikeShe counts the layers in a DFS3-file from the top down starting with the upper most layer as number 1. Other MikeZero products counts from the bottom most layer and starts at 0. The MikeSheWrapper also counts the lower layer as 0.

The third difference is that MikeShe uses the x-origin of a grid as the left most side of the grid whereas MikeZero uses the x-origin as the center of the left most grid block. The same goes for the y-origin. This difference is more severe because the data is now interpreted to be situated differently. This means that if you open a resultfile from MikeShe in the grid editor the coordinates will be half a cell wrong.
Everything in the MikeSheWrapper related to MikeShe does it like MikeShe. However, if you use the DFS-classes directly they will behave like MikeZero.

In the MikeZero toolbox the tool MikeToGrid will correctly place the MikeShe results when imported in ArcGIS. The tool MikeToShape will place the results half a gridblock wrong.

If you want to export MikeShe results to shapes it is better to use the MikeSheWrapper.