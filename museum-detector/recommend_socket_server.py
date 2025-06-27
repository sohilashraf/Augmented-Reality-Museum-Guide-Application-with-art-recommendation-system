import socket
import json

def get_recommendations(user_id):
    return ["Thutmose", "Khonsu", "Khedive_Ismail"]

HOST = "0.0.0.0"
PORT = 5051

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind((HOST, PORT))
server.listen(1)
print(f"🟢 Recommendation Server is listening on {PORT}...")

while True:
    conn, addr = server.accept()
    print(f"📥 Connected by {addr}")
    
    data = conn.recv(1024).decode()
    print(f"🔍 Received user_id: {data}")

    recommendations = get_recommendations(data.strip())
    response = json.dumps(recommendations)
    conn.sendall(response.encode())
    conn.close()
