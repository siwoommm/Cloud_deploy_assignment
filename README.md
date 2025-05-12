# 🧭 Indoor AR Navigation Project

QR 코드 기반 위치 인식과 경량화된 VIO 알고리즘을 이용한 실내 AR 내비게이션 시스템 개발

---

## 📌 프로젝트 개요

본 프로젝트는 실내 공간에서 QR 코드를 통한 정밀한 위치 인식과,  
경량화된 VIO(Visual-Inertial Odometry) 기반 이동 추정 기술을 결합하여  
**모바일 디바이스 환경에서도 원활히 동작하는 실내 AR 내비게이션 앱**을 개발하는 것을 목표로 합니다.

- QR코드 + VIO 기반 위치 보정
- 2D 노드맵 모델링 및 A* 경로 최적화 탐색
- Unity 기반 AR 시각화
- 사용자 친화적인 UX/UI 통합

![image](https://github.com/user-attachments/assets/d5c3e140-323f-4905-96a1-4f3543afd6f8)

---

## 🏛️ 프로젝트 추진 배경

- **GPS가 닿지 않는 실내 공간**에서 직관적인 경로 안내 제공
- 기존 비콘, SLAM 방식의 한계(설치비용, 연산부담 등) 극복
- BIM 데이터 기반 3D 맵 활용 및 경량화된 VIO 적용
- 모바일에서도 실시간 가능한 실내 길 안내 구현

---

## 🎯 주요 목표

- QR 코드 기반 실내 위치 초기화 및 실시간 업데이트
- A* 알고리즘을 통한 최단 경로 탐색
- 향후 대형 맵 대응을 위한 Jump Point Search(JPS) 최적화 연구
- 경량화된 VIO 알고리즘과 AR Foundation 기반 경로 시각화 통합
- Unity3D 기반 모바일 애플리케이션 개발

---

## 🔧 주요 기술 스택

- **Unity (C#) + AR Foundation**  
- **ZXing.NET** (QR 코드 디코딩)
- **OpenCV** (추후 경량화된 VIO 테스트 예정)
- **BIM 기반 2D/3D 맵 활용**
- **A\* 알고리즘 (추후 JPS 확장 고려)**

---


## 👥 팀원 소개

| 이름 | 역할 |
|:---|:---|
| 이예진 (팀장) | QR/VIO 모듈 개발, 시스템 통합 |
| 이시우 | 2D 맵 구축, A* 경로 탐색 최적화 |
| 서도윤 | AR 시각화, UX/UI 설계 |

---

## Reference
- [ARKit - ARCore - Indoor Navigation](https://www.youtube.com/watch?v=H5utsMbeNuw&list=PLHYDWlZPYhLI0JM-3gsFFq3x0laxhzbTL)
---

## Notion link
https://www.notion.so/Indoor-AR-Navigation-Project-1f11c888d21a80389b3bcf9e26117dd2?pvs=4 
