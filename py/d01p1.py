import sys
from common import calibration_value

if __name__ == '__main__':
    with open(sys.argv[1]) as lines:
        print(sum(map(calibration_value, lines)))
