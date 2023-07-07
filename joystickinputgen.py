
# Use the results from this file to update inputManager.asset to add more joystick and axes if ever needed.

f = open("joystickinputsettings.txt", 'w')


for joystick in [1,2,3,4,5,6,7,8]:
    for axis in [1,2,3,4,5,6,7,8,9,10,11,12]:
        f.write(f'''  - serializedVersion: 3
    m_Name: joystick {joystick} axis {axis}
    descriptiveName: 
    descriptiveNegativeName: 
    negativeButton: 
    positiveButton: 
    altNegativeButton: 
    altPositiveButton: 
    gravity: 0
    dead: 0.5
    sensitivity: 1
    snap: 0
    invert: 0
    type: 2
    axis: {axis - 1}
    joyNum: {joystick}\n''')

f.close()