import { useState, useEffect } from 'react'
import { Button } from '@/components/ui/button.jsx'
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from '@/components/ui/card.jsx'
import { Badge } from '@/components/ui/badge.jsx'
import { Heart, Calendar, MapPin, Clock, Users, Gift, Camera, Shirt, ChevronDown, Star } from 'lucide-react'
import { motion, AnimatePresence } from 'framer-motion'
import './App.css'

function App() {
  const [activeSection, setActiveSection] = useState('inicio')
  const [timeLeft, setTimeLeft] = useState({
    days: 365,
    hours: 24,
    minutes: 60,
    seconds: 60
  })

  // Countdown timer
  useEffect(() => {
    const targetDate = new Date('2026-06-15T15:00:00')
    
    const timer = setInterval(() => {
      const now = new Date().getTime()
      const distance = targetDate.getTime() - now
      
      const days = Math.floor(distance / (1000 * 60 * 60 * 24))
      const hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60))
      const minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60))
      const seconds = Math.floor((distance % (1000 * 60)) / 1000)
      
      setTimeLeft({ days, hours, minutes, seconds })
    }, 1000)

    return () => clearInterval(timer)
  }, [])

  const sections = [
    { id: 'inicio', label: 'Início', icon: Heart },
    { id: 'historia', label: 'Nossa História', icon: Star },
    { id: 'detalhes', label: 'Detalhes', icon: Calendar },
    { id: 'dresscode', label: 'Dress Code', icon: Shirt },
    { id: 'galeria', label: 'Galeria', icon: Camera },
    { id: 'presentes', label: 'Presentes', icon: Gift }
  ]

  const scrollToSection = (sectionId) => {
    setActiveSection(sectionId)
    const element = document.getElementById(sectionId)
    if (element) {
      element.scrollIntoView({ behavior: 'smooth' })
    }
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-slate-50 via-blue-50/30 to-slate-100">
      {/* Navigation */}
      <nav className="fixed top-0 left-0 right-0 z-50 bg-white/90 backdrop-blur-md border-b border-slate-200/50 shadow-sm">
        <div className="container mx-auto px-4 py-4">
          <div className="flex items-center justify-between">
            <motion.h1 
              className="text-2xl font-light text-slate-700 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
              initial={{ opacity: 0, x: -20 }}
              animate={{ opacity: 1, x: 0 }}
            >
              Artur & Sara
            </motion.h1>
            
            <div className="hidden md:flex space-x-8">
              {sections.map((section, index) => {
                const Icon = section.icon
                return (
                  <motion.button
                    key={section.id}
                    onClick={() => scrollToSection(section.id)}
                    className={`flex items-center space-x-2 px-4 py-2 rounded-full transition-all duration-300 ${
                      activeSection === section.id 
                        ? 'bg-slate-100 text-slate-700' 
                        : 'text-slate-500 hover:text-slate-700 hover:bg-slate-50'
                    }`}
                    initial={{ opacity: 0, y: -20 }}
                    animate={{ opacity: 1, y: 0 }}
                    transition={{ delay: index * 0.1 }}
                  >
                    <Icon size={16} />
                    <span className="text-sm font-medium">{section.label}</span>
                  </motion.button>
                )
              })}
            </div>
          </div>
        </div>
      </nav>

      {/* Hero Section */}
      <section id="inicio" className="min-h-screen flex items-center justify-center relative overflow-hidden pt-20">
        <div className="absolute inset-0 bg-gradient-to-br from-slate-100/50 via-blue-50/30 to-slate-200/40"></div>
        <div className="container mx-auto px-4 text-center relative z-10">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.8 }}
            className="max-w-4xl mx-auto"
          >
            <h1 
              className="text-6xl md:text-8xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Artur & Sara
            </h1>
            <p className="text-xl md:text-2xl text-slate-600 mb-8 font-light tracking-wide">
              Vamos nos casar!
            </p>
            <div className="flex items-center justify-center space-x-3 mb-12">
              <Calendar className="text-slate-500" size={20} />
              <span className="text-lg md:text-xl font-light text-slate-600 tracking-wide">
                15 de Junho de 2026
              </span>
            </div>
            
            <Button 
              onClick={() => scrollToSection('presentes')}
              className="bg-slate-600 hover:bg-slate-700 text-white px-8 py-3 text-base rounded-full shadow-lg hover:shadow-xl transition-all duration-300 font-light tracking-wide"
            >
              Ver Lista de Presentes
            </Button>
          </motion.div>
        </div>
        
        <motion.div 
          className="absolute bottom-8 left-1/2 transform -translate-x-1/2"
          animate={{ y: [0, 10, 0] }}
          transition={{ repeat: Infinity, duration: 2 }}
        >
          <ChevronDown className="text-slate-400" size={28} />
        </motion.div>
      </section>

      {/* Welcome Section */}
      <section className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="max-w-4xl mx-auto text-center"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-8 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Bem-vindo ao Nosso Site de Casamento
            </h2>
            <p className="text-lg text-slate-600 mb-12 leading-relaxed font-light">
              Estamos animados para celebrar nosso dia especial com você! Como estamos nos preparando para nos mudar para outro país após o casamento, criamos este site para compartilhar nossa jornada e proporcionar uma maneira de você fazer parte do nosso novo começo.
            </p>
            
            <div className="grid md:grid-cols-2 gap-8">
              <Card className="border border-slate-200 hover:border-slate-300 transition-colors duration-300 shadow-sm hover:shadow-md">
                <CardHeader>
                  <CardTitle className="flex items-center space-x-3 text-slate-700">
                    <Heart className="text-slate-500" size={20} />
                    <span className="font-light">A Cerimônia</span>
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <p className="text-slate-600 mb-4 font-light">
                    Junte-se a nós enquanto trocamos votos e celebramos nosso amor.
                  </p>
                  <Button 
                    variant="outline" 
                    onClick={() => scrollToSection('detalhes')}
                    className="border-slate-300 text-slate-600 hover:bg-slate-50 font-light"
                  >
                    Ver Detalhes →
                  </Button>
                </CardContent>
              </Card>
              
              <Card className="border border-slate-200 hover:border-slate-300 transition-colors duration-300 shadow-sm hover:shadow-md">
                <CardHeader>
                  <CardTitle className="flex items-center space-x-3 text-slate-700">
                    <Gift className="text-slate-500" size={20} />
                    <span className="font-light">Lista de Presentes</span>
                  </CardTitle>
                </CardHeader>
                <CardContent>
                  <p className="text-slate-600 mb-4 font-light">
                    Ajude-nos a começar nossa nova vida no exterior com presentes simbólicos.
                  </p>
                  <Button 
                    variant="outline"
                    onClick={() => scrollToSection('presentes')}
                    className="border-slate-300 text-slate-600 hover:bg-slate-50 font-light"
                  >
                    Ver Lista →
                  </Button>
                </CardContent>
              </Card>
            </div>
          </motion.div>
        </div>
      </section>

      {/* Countdown Section */}
      <section className="py-20 bg-gradient-to-r from-slate-600 to-slate-700 text-white">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center"
          >
            <h2 
              className="text-4xl md:text-5xl font-light mb-12 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Contagem Regressiva para o Grande Dia
            </h2>
            
            <div className="grid grid-cols-2 md:grid-cols-4 gap-8 max-w-4xl mx-auto">
              {[
                { value: timeLeft.days, label: 'Dias' },
                { value: timeLeft.hours, label: 'Horas' },
                { value: timeLeft.minutes, label: 'Minutos' },
                { value: timeLeft.seconds, label: 'Segundos' }
              ].map((item, index) => (
                <motion.div
                  key={item.label}
                  initial={{ opacity: 0, scale: 0.5 }}
                  whileInView={{ opacity: 1, scale: 1 }}
                  viewport={{ once: true }}
                  transition={{ delay: index * 0.1 }}
                  className="bg-white/10 backdrop-blur-sm rounded-2xl p-6 border border-white/20"
                >
                  <div className="text-4xl md:text-6xl font-light mb-2">
                    {item.value}
                  </div>
                  <div className="text-lg font-light opacity-90">
                    {item.label}
                  </div>
                </motion.div>
              ))}
            </div>
          </motion.div>
        </div>
      </section>

      {/* Nossa História */}
      <section id="historia" className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center mb-16"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Nossa História
            </h2>
            <p className="text-lg text-slate-600 max-w-2xl mx-auto font-light">
              Como nos conhecemos, nos apaixonamos e decidimos passar nossas vidas juntos
            </p>
          </motion.div>

          <div className="max-w-4xl mx-auto space-y-12">
            {[
              {
                title: "Como Nos Conhecemos",
                date: "Janeiro de 2020",
                content: "Nos encontramos pela primeira vez na festa de aniversário de um amigo em comum. Sara foi imediatamente atraída pelo senso de humor de Artur, enquanto Artur não conseguia tirar os olhos do sorriso de Sara. Depois de conversarmos por horas naquela noite, soubemos que havia algo especial entre nós."
              },
              {
                title: "Primeiro Encontro",
                date: "Fevereiro de 2020",
                content: "Nosso primeiro encontro foi em um pequeno café no centro da cidade. O que deveria ser um café rápido se transformou em uma conversa de cinco horas. Falamos sobre tudo, desde nossos sonhos de infância até nossos livros favoritos. No final da noite, ambos sabíamos que este era o começo de algo maravilhoso."
              },
              {
                title: "O Pedido",
                date: "Dezembro de 2024",
                content: "Artur fez o pedido durante um fim de semana surpresa nas montanhas. Após uma bela caminhada até um mirante cênico, ele se ajoelhou e pediu Sara em casamento. Com lágrimas de alegria, ela disse sim! Celebramos com champanhe enquanto o sol se punha sobre as montanhas, marcando o início de nossa jornada rumo ao casamento."
              }
            ].map((story, index) => (
              <motion.div
                key={index}
                initial={{ opacity: 0, x: index % 2 === 0 ? -50 : 50 }}
                whileInView={{ opacity: 1, x: 0 }}
                viewport={{ once: true }}
                transition={{ delay: index * 0.2 }}
                className={`flex flex-col md:flex-row items-center gap-8 ${
                  index % 2 === 1 ? 'md:flex-row-reverse' : ''
                }`}
              >
                <div className="flex-1">
                  <Card className="border border-slate-200 hover:shadow-lg transition-shadow duration-300 bg-white">
                    <CardHeader>
                      <div className="flex items-center justify-between">
                        <CardTitle className="text-2xl text-slate-700 font-light">
                          {story.title}
                        </CardTitle>
                        <Badge variant="secondary" className="bg-slate-100 text-slate-600 font-light">
                          {story.date}
                        </Badge>
                      </div>
                    </CardHeader>
                    <CardContent>
                      <p className="text-slate-600 leading-relaxed font-light">
                        {story.content}
                      </p>
                    </CardContent>
                  </Card>
                </div>
                <div className="w-32 h-32 bg-gradient-to-br from-slate-100 to-slate-200 rounded-full flex items-center justify-center border border-slate-200">
                  <Heart className="text-slate-500" size={40} />
                </div>
              </motion.div>
            ))}
          </div>
        </div>
      </section>

      {/* Detalhes do Casamento */}
      <section id="detalhes" className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center mb-16"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Detalhes do Casamento
            </h2>
            <p className="text-lg text-slate-600 font-light">
              Tudo o que você precisa saber sobre nosso dia especial
            </p>
          </motion.div>

          <div className="max-w-6xl mx-auto grid md:grid-cols-2 gap-8 mb-16">
            <Card className="border border-slate-200 shadow-sm">
              <CardHeader>
                <CardTitle className="flex items-center space-x-3 text-slate-700 font-light">
                  <Calendar className="text-slate-500" size={20} />
                  <span>Data e Hora</span>
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <div>
                  <p className="text-2xl font-light text-slate-700">15 de Junho de 2026</p>
                </div>
                <div className="space-y-2">
                  <div className="flex items-center space-x-2">
                    <Clock size={16} className="text-slate-400" />
                    <span className="font-light text-slate-600">Cerimônia: 15:00</span>
                  </div>
                  <div className="flex items-center space-x-2">
                    <Users size={16} className="text-slate-400" />
                    <span className="font-light text-slate-600">Recepção: 17:00</span>
                  </div>
                </div>
              </CardContent>
            </Card>

            <Card className="border border-slate-200 shadow-sm">
              <CardHeader>
                <CardTitle className="flex items-center space-x-3 text-slate-700 font-light">
                  <MapPin className="text-slate-500" size={20} />
                  <span>Local</span>
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-2">
                <p className="text-xl font-light text-slate-700">Jardins de Cristal</p>
                <p className="text-slate-600 font-light">Rua dos Noivos, 123</p>
                <p className="text-slate-600 font-light">Beira-Lago, SP</p>
              </CardContent>
            </Card>
          </div>

          <Card className="max-w-4xl mx-auto border border-slate-200 shadow-sm">
            <CardHeader>
              <CardTitle className="text-2xl text-center text-slate-700 font-light">Programação do Dia</CardTitle>
            </CardHeader>
            <CardContent>
              <div className="space-y-6">
                {[
                  { time: "14:30", title: "Chegada dos Convidados", desc: "Por favor, chegue 30 minutos antes do início da cerimônia para ser acomodado." },
                  { time: "15:00", title: "Cerimônia", desc: "A cerimônia acontecerá no Pavilhão do Jardim." },
                  { time: "16:00", title: "Coquetel", desc: "Aproveite bebidas e canapés no Terraço à Beira do Lago." },
                  { time: "17:00", title: "Recepção", desc: "Jantar e dança no Salão Principal." }
                ].map((event, index) => (
                  <motion.div
                    key={index}
                    initial={{ opacity: 0, x: -20 }}
                    whileInView={{ opacity: 1, x: 0 }}
                    viewport={{ once: true }}
                    transition={{ delay: index * 0.1 }}
                    className="flex items-start space-x-4 p-4 rounded-lg hover:bg-slate-50 transition-colors duration-300"
                  >
                    <div className="bg-slate-100 text-slate-600 px-3 py-1 rounded-full font-light min-w-fit">
                      {event.time}
                    </div>
                    <div>
                      <h4 className="font-light text-slate-700 mb-1">{event.title}</h4>
                      <p className="text-slate-600 font-light">{event.desc}</p>
                    </div>
                  </motion.div>
                ))}
              </div>
            </CardContent>
          </Card>
        </div>
      </section>

      {/* Dress Code */}
      <section id="dresscode" className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center mb-16"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Dress Code
            </h2>
            <p className="text-lg text-slate-600 font-light">
              O que vestir para nossa celebração
            </p>
          </motion.div>

          <div className="max-w-4xl mx-auto">
            <Card className="border border-slate-200 mb-8 shadow-sm bg-white">
              <CardHeader className="text-center">
                <CardTitle className="text-3xl font-light text-slate-700">Traje Social</CardTitle>
                <CardDescription className="text-lg font-light text-slate-600">
                  Convidamos você a se juntar a nós em traje social para nossa celebração de casamento. 
                  Queremos que todos se sintam confortáveis, mantendo uma atmosfera elegante para nosso dia especial.
                </CardDescription>
              </CardHeader>
              <CardContent className="text-center">
                <p className="text-lg font-light text-slate-600 mb-8">
                  Nossas cores são <span className="font-medium text-slate-700">Azul Serenidade e Branco</span>
                </p>
                
                <div className="grid md:grid-cols-2 gap-8">
                  <Card className="border border-slate-200 shadow-sm">
                    <CardHeader>
                      <CardTitle className="flex items-center justify-center space-x-2 text-slate-700 font-light">
                        <Shirt className="text-slate-500" size={20} />
                        <span>Para Homens</span>
                      </CardTitle>
                    </CardHeader>
                    <CardContent className="space-y-4">
                      <ul className="text-left space-y-2 font-light text-slate-600">
                        <li>• Terno ou calça social com camisa</li>
                        <li>• Paletó ou blazer (opcional)</li>
                        <li>• Gravata (opcional)</li>
                        <li>• Sapatos sociais</li>
                      </ul>
                      <div className="bg-slate-50 p-4 rounded-lg">
                        <h4 className="font-medium mb-2 text-slate-700">Sugestões de Cores</h4>
                        <p className="text-sm text-slate-600 font-light">
                          Ternos/calças em azul marinho, cinza ou preto. Camisas em azul claro ou branco são bem-vindas para complementar nossas cores.
                        </p>
                      </div>
                    </CardContent>
                  </Card>

                  <Card className="border border-slate-200 shadow-sm">
                    <CardHeader>
                      <CardTitle className="flex items-center justify-center space-x-2 text-slate-700 font-light">
                        <Shirt className="text-slate-500" size={20} />
                        <span>Para Mulheres</span>
                      </CardTitle>
                    </CardHeader>
                    <CardContent className="space-y-4">
                      <ul className="text-left space-y-2 font-light text-slate-600">
                        <li>• Vestido de festa ou macacão elegante</li>
                        <li>• Conjuntos sociais (saia/calça com blusa elegante)</li>
                        <li>• Saltos, sapatilhas elegantes ou sandálias sociais</li>
                        <li>• Xale ou estola leve (opcional, para a noite)</li>
                      </ul>
                      <div className="bg-slate-50 p-4 rounded-lg">
                        <h4 className="font-medium mb-2 text-slate-700">Sugestões de Cores</h4>
                        <p className="text-sm text-slate-600 font-light">
                          Qualquer cor exceto branco (reservado para a noiva). Azul serenidade, tons pastéis ou cores joia são bem-vindos.
                        </p>
                      </div>
                    </CardContent>
                  </Card>
                </div>
              </CardContent>
            </Card>
          </div>
        </div>
      </section>

      {/* Galeria */}
      <section id="galeria" className="py-20 bg-white">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center mb-16"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Galeria de Fotos
            </h2>
            <p className="text-lg text-slate-600 font-light">
              Memórias que compartilhamos juntos
            </p>
          </motion.div>

          <div className="max-w-4xl mx-auto mb-12">
            <p className="text-center text-slate-600 mb-8 font-light">
              Esta galeria será preenchida com nossas fotos favoritas após o casamento. 
              Por enquanto, aprecie alguns momentos de nossa jornada juntos.
            </p>
            
            <div className="grid grid-cols-2 md:grid-cols-3 gap-4">
              {[
                "Nosso Primeiro Encontro",
                "Férias de Verão 2023",
                "Celebração de Fim de Ano",
                "Aventura de Trilha",
                "Dia do Noivado",
                "Reunião Familiar"
              ].map((title, index) => (
                <motion.div
                  key={index}
                  initial={{ opacity: 0, scale: 0.8 }}
                  whileInView={{ opacity: 1, scale: 1 }}
                  viewport={{ once: true }}
                  transition={{ delay: index * 0.1 }}
                  className="aspect-square bg-gradient-to-br from-slate-100 to-slate-200 rounded-lg flex items-center justify-center hover:shadow-lg transition-shadow duration-300 border border-slate-200"
                >
                  <div className="text-center p-4">
                    <Camera className="mx-auto mb-2 text-slate-400" size={28} />
                    <p className="text-sm font-light text-slate-600">{title}</p>
                  </div>
                </motion.div>
              ))}
            </div>
          </div>

          <Card className="max-w-2xl mx-auto border border-slate-200 shadow-sm">
            <CardHeader>
              <CardTitle className="text-center text-slate-700 font-light">Compartilhe Suas Fotos</CardTitle>
            </CardHeader>
            <CardContent className="text-center space-y-4">
              <p className="text-slate-600 font-light">
                Adoraríamos ver o casamento através dos seus olhos! Após a celebração, 
                compartilhe suas fotos conosco usando a hashtag:
              </p>
              <div className="bg-slate-50 p-4 rounded-lg">
                <p 
                  className="text-2xl font-light text-slate-700"
                  style={{ fontFamily: "'Dancing Script', cursive" }}
                >
                  #ArturESara2026
                </p>
              </div>
              <p className="text-sm text-slate-500 font-light">
                Estaremos coletando fotos das redes sociais para adicionar à nossa galeria. 
                Se preferir compartilhar de forma privada, você pode enviar suas fotos para fotos@exemplo.com
              </p>
            </CardContent>
          </Card>
        </div>
      </section>

      {/* Lista de Presentes */}
      <section id="presentes" className="py-20 bg-slate-50">
        <div className="container mx-auto px-4">
          <motion.div
            initial={{ opacity: 0, y: 50 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            className="text-center mb-16"
          >
            <h2 
              className="text-4xl md:text-5xl font-light text-slate-700 mb-6 tracking-wide"
              style={{ fontFamily: "'Dancing Script', cursive" }}
            >
              Lista de Presentes
            </h2>
            <p className="text-lg text-slate-600 font-light">
              Ajude-nos a começar nossa nova jornada juntos
            </p>
          </motion.div>

          <div className="max-w-4xl mx-auto">
            <Card className="border border-slate-200 mb-12 shadow-sm bg-white">
              <CardHeader>
                <CardTitle className="text-2xl text-center text-slate-700 font-light">Nossa Nova Aventura</CardTitle>
              </CardHeader>
              <CardContent className="text-center space-y-4">
                <p className="text-slate-600 leading-relaxed font-light">
                  Como estamos nos preparando para nos mudar para outro país após o casamento, 
                  criamos esta lista de presentes simbólicos para nos ajudar a começar nossa nova vida juntos. 
                  Sua contribuição, independentemente do tamanho, será profundamente apreciada e nos ajudará 
                  a estabelecer nosso lar no exterior.
                </p>
                <p className="text-sm text-slate-500 font-light">
                  Todos os pagamentos são processados com segurança através do MercadoPago, 
                  garantindo que sua transação seja segura e confiável.
                </p>
              </CardContent>
            </Card>

            <div className="grid md:grid-cols-2 gap-6 mb-12">
              {[
                {
                  title: "Primeiro Mês de Aluguel",
                  description: "Ajude-nos a garantir nossa primeira casa em nosso novo país.",
                  price: "R$ 7.500",
                  available: "5 disponíveis"
                },
                {
                  title: "Móveis Novos",
                  description: "Ajude-nos a mobiliar nossa nova casa com peças essenciais.",
                  price: "R$ 2.500",
                  available: "10 disponíveis"
                },
                {
                  title: "Essenciais para Cozinha",
                  description: "Ajude-nos a equipar nossa cozinha com eletrodomésticos e utensílios.",
                  price: "R$ 1.500",
                  available: "8 disponíveis"
                },
                {
                  title: "Valor Personalizado",
                  description: "Contribua com qualquer valor que desejar para nos ajudar a começar nossa nova vida.",
                  price: "Livre",
                  available: "Sempre disponível"
                }
              ].map((gift, index) => (
                <motion.div
                  key={index}
                  initial={{ opacity: 0, y: 20 }}
                  whileInView={{ opacity: 1, y: 0 }}
                  viewport={{ once: true }}
                  transition={{ delay: index * 0.1 }}
                >
                  <Card className="border border-slate-200 hover:border-slate-300 hover:shadow-lg transition-all duration-300 h-full bg-white">
                    <CardHeader>
                      <CardTitle className="text-xl text-slate-700 font-light">{gift.title}</CardTitle>
                      <CardDescription className="font-light">{gift.description}</CardDescription>
                    </CardHeader>
                    <CardContent className="space-y-4">
                      <div className="flex justify-between items-center">
                        <span className="text-2xl font-light text-slate-700">{gift.price}</span>
                        <Badge variant="secondary" className="bg-slate-100 text-slate-600 font-light">{gift.available}</Badge>
                      </div>
                      <Button className="w-full bg-slate-600 hover:bg-slate-700 text-white font-light">
                        Contribuir
                      </Button>
                    </CardContent>
                  </Card>
                </motion.div>
              ))}
            </div>

            <Card className="border border-slate-200 bg-white shadow-sm">
              <CardHeader>
                <CardTitle className="text-center text-2xl text-slate-700 font-light">Obrigado</CardTitle>
              </CardHeader>
              <CardContent className="text-center space-y-4">
                <p className="text-slate-600 leading-relaxed font-light">
                  Sua generosidade significa o mundo para nós. Cada contribuição nos ajuda a construir 
                  nossa nova vida juntos em nosso novo país. Estamos ansiosos para compartilhar atualizações 
                  com você sobre nossa jornada e como seus presentes nos ajudaram ao longo do caminho.
                </p>
                <p 
                  className="text-lg font-light text-slate-700"
                  style={{ fontFamily: "'Dancing Script', cursive" }}
                >
                  Com amor e gratidão,<br />
                  Artur & Sara
                </p>
              </CardContent>
            </Card>
          </div>
        </div>
      </section>

      {/* Footer */}
      <footer className="bg-slate-700 text-white py-8">
        <div className="container mx-auto px-4 text-center">
          <p className="text-slate-300 font-light">
            Made with ❤️ for Artur & Sara's Wedding
          </p>
        </div>
      </footer>
    </div>
  )
}

export default App

