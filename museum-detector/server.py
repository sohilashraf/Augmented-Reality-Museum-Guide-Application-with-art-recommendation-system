import socket
import struct
import cv2
import numpy as np
from ultralytics import YOLO

model = YOLO("best.pt")  

def handle_client(conn):
    try:
        length_buf = conn.recv(4)
        if not length_buf:
            return
        length, = struct.unpack('!I', length_buf)
        data = b''
        while len(data) < length:
            more = conn.recv(length - len(data))
            if not more:
                return
            data += more

        img_array = np.frombuffer(data, dtype=np.uint8)
        img = cv2.imdecode(img_array, cv2.IMREAD_COLOR)

        results = model(img)[0]

        thresholds = {
            "Khedive_Ismail": 0.8,
            "Khonsu": 0.7,
            "Thutmose": 0.7
        }

        best_label = "none"
        best_score = 0

        for box in results.boxes:
            class_id = int(box.cls[0])
            score = float(box.conf[0])
            label = model.names[class_id]

            if label in thresholds and score >= thresholds[label] and score > best_score:
                best_label = label
                best_score = score

        print(f"📦 Detection result: {best_label} (score: {best_score:.2f})")
        conn.sendall(best_label.encode("utf-8"))

    except Exception as e:
        print("❌ Error in handle_client:", e)
        try:
            conn.sendall(b"none")
        except:
            pass
    finally:
        conn.close()

def start_server(host='0.0.0.0', port=5001):
    server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server.bind((host, port))
    server.listen(1)
    print(f"🚀 TCP Server listening on {host}:{port}")

    while True:
        conn, addr = server.accept()
        print(f"🔌 Connection from {addr}")
        handle_client(conn)

if __name__ == "__main__":
    start_server()
