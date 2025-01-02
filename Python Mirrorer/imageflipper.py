import subprocess
import sys, os
import pkg_resources

def flip_image(input_path: str, output_path: str):
    if not os.path.isfile(input_path):
        print(f"{input_path} is not a file...skipping")
        return

    image = Image.open(input_path)
    mirrored_image = image.transpose(Image.FLIP_LEFT_RIGHT)
    if not output_path.endswith("/"):
        output_path += "/"

    mirrored_image.save(output_path + os.path.basename(input_path))

installed = {pkg.key for pkg in pkg_resources.working_set}
def ensure_library_installed(libraries: set):
    missing = libraries - installed
    if len(missing) > 0:
        subprocess.check_call([sys.executable, '-m', 'pip', 'install', missing], stdout=subprocess.DEVNULL)

if __name__ == "__main__":
    input_images = sys.argv[1:-1]
    output_path = sys.argv[-1]
    if not os.path.isdir(output_path):
        print("output path is not directory")
        sys.exit(1)

    # Check for Pillow (Pillow is the library name in pip, but the module to import is PIL)
    ensure_library_installed({"pillow"})

    from PIL import Image
    for image in input_images:
        flip_image(image, output_path)
