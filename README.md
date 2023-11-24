# Port Scanner

This project is a port scanner implemented in C#. It allows you to scan a specific host to check which ports are open.

## Features

- Scan a single port, a range of ports, or all ports.
- Write scan results to a file.
- Supports both hostnames and IP addresses.

## Usage

1. Open a command prompt as administrator.
2. Navigate to the directory containing the utility.
3. Run the utility as a command-line argument. For example:

    ```
    .\PortScanner.exe
    ```
4. Enter the host to scan. This can be either a hostname or an IP address.
5. Choose the scanning strategy:
   - Enter `1` to scan a single port.
   - Enter `2` to scan a range of ports.
   - Enter `3` to scan all ports.
6. If you chose to scan a single port or a range of ports, you will be prompted to enter the port(s).
7. The program will scan the specified host and ports, and write the results to a file on your desktop.


## Author

Bohdan Harabadzhyu

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](https://choosealicense.com/licenses/mit/)
