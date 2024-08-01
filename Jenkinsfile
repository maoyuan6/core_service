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
               echo 'build project'
               sh 'docker compose -f docker_compose.yml up --build -d' 
            }
        }
        stage('publish') {
            steps {
                echo 'publish'
                sh 'success'
            }
        }
    }
}
