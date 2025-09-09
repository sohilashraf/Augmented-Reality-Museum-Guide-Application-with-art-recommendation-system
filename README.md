# 🏛️ AR Museum Guide & Recommendation System
An **Augmented Reality museum application** that enhances the visitor experience by combining **AI-powered object detection** with **interactive 3D models** and a **smart recommendation engine**.
## ✨ Features
- **Real-time Exhibit Detection**: Identify museum artifacts using **YOLOv11** optimized for mobile devices.  
- **Immersive AR Experience**: Developed in **Unity (AR Foundation + URP)** to overlay interactive **3D models** on exhibits, with pinch-to-zoom and rotation gestures.  
- **Narrated Storytelling**: A **virtual ancient Egyptian avatar** guides visitors with synchronized audio narration.  
- **Personalized Recommendations**: A hybrid recommendation engine suggests related exhibits based on **text, images, and user interactions**.  
- **Prototype Deployment**: Successfully tested on selected artifacts in the **The National Museum of Egyptian Civilization**.  


## 📊 Dataset
This project combines multiple data sources to enable both object detection and personalized recommendations:  

- **Exhibit Metadata**: Titles, categories, historical periods, and bilingual descriptions (English & Arabic).  
- **Text Features**: Extracted using **BERT embeddings** for semantic understanding of exhibit descriptions.  
- **Image Features**: Extracted using **ResNet50** from a **diverse image dataset** that includes artifacts, statues, and mockups of historical buildings, enabling robust visual similarity matching across exhibits.
<img width="950" height="437" alt="image" src="https://github.com/user-attachments/assets/257231e6-2c16-4eb0-a722-164ec6f3b0aa" />

- **Ratings Dataset**: Simulated user ratings (likes/dislikes and numeric scores) to train collaborative filtering models.  
- **YOLO Training Data**: A **custom image dataset** created by capturing photos of real museum artifacts using **mobile phones and professional cameras**.  
  - Images were further **preprocessed, annotated, and augmented in Roboflow**.  
  - Includes statues like the **Thutmose III**, **Khonsu**, and **khedive ismail**.  
<img width="950" height="510" alt="image" src="https://github.com/user-attachments/assets/bab183e6-6ab8-44c1-a83a-ab8b707b4b65" />


## 🎥 Demo


https://github.com/user-attachments/assets/01fe8f84-fa63-4f40-94af-2091bfbbe0d3

https://github.com/user-attachments/assets/3a2b36a0-acc5-4cc0-a0e0-17742af10543

https://github.com/user-attachments/assets/a8a16011-5f71-4041-8545-74269e9f84e7

## 📖 Publication

This project was presented as a paper at the:

**3rd International IEEE Conference for Intelligent Methods, Systems, and Applications (IMSA 2025)**  

**Paper Title:**  
*Museum Virtual Guide with Deep Learning-Based Hybrid Recommendations for Personalized User Experiences*  

<img width="784" height="550" alt="image" src="https://github.com/user-attachments/assets/7d17ebf4-491f-4d96-beb0-ddcaa4da1468" />


