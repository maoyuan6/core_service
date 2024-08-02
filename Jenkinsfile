pipeline {
    agent any
    stages {
        stage('pull code') {
            steps {
                git credentialsId: 'maoyuan', url: 'https://github.com/maoyuan6/core_service.git' 
            }
        }
        stage('build project') {
            steps {
               echo 'Docker and Docker Compose version:'
                // 输出 Docker 版本
                sh 'docker --version'
                // 输出 Docker Compose 版本
                sh 'docker compose version'
                echo 'Building and starting Docker containers...'
                sh 'docker compose -f docker_compose.yml up --build -d'
            }
        }
        stage('publish') {
            steps { 
				echo 'success' 
            }
        }
    }
}
