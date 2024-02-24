import base64
import cv2
import numpy as np
import sys

def generate_single_marker(arucoid):
    marker_size = int(100)
    aruco_dict = cv2.aruco.getPredefinedDictionary(cv2.aruco.DICT_7X7_1000)

    marker_img = cv2.aruco.generateImageMarker(aruco_dict, arucoid, marker_size)

    # Convert the image to a byte stream
    retval, buffer = cv2.imencode('.png', marker_img)
    if not retval:
        raise Exception("Could not encode image to PNG format")

    # Encode the byte stream to base64
    base64_str = base64.b64encode(buffer).decode('utf-8')

    return base64_str

def main():
    markerValue = sys.argv[1]
    # if markerValue is not a number, exit
    if not markerValue.isdigit():
        print("Invalid input")
        return
    markerValue = int(markerValue)
    # if markerValue is not in the range of  1 to  999, exit
    if markerValue <  0 or markerValue >  999:
        print("Invalid input")
        return
    base64_marker = generate_single_marker(markerValue)
    print(base64_marker)

if __name__ == "__main__":
    main()
