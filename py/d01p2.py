import sys
from common import better_calibration_value

if __name__ == '__main__':
    with open(sys.argv[1]) as lines:
        print(sum(map(better_calibration_value, lines)))
