package com.github.fantasy0v0.netty;

import io.netty.bootstrap.ServerBootstrap;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioServerSocketChannel;
import io.netty.handler.codec.LengthFieldBasedFrameDecoder;

import java.io.IOException;
import java.security.Provider;

public class Application {

  public static void main(String[] args) throws InterruptedException, IOException {
    /*File file = new File("C:/Users/fan/Downloads/二维码商户录入指引.pdf");
    PDDocument document = Loader.loadPDF(file);
    PDFTextStripper stripper = new PDFTextStripper();
    System.out.println(stripper.getText(document));*/

    EventLoopGroup bossGroup = new NioEventLoopGroup(); // (1)
    EventLoopGroup workerGroup = new NioEventLoopGroup();
    ServerBootstrap b = new ServerBootstrap(); // (2)
    b.group(bossGroup, workerGroup)
        .channel(NioServerSocketChannel.class) // (3)
        .childHandler(new ChannelInitializer<SocketChannel>() { // (4)
          @Override
          public void initChannel(SocketChannel ch) throws Exception {
            ch.pipeline()
              .addLast(new LengthFieldBasedFrameDecoder(Integer.MAX_VALUE, 0, 2, 0, 2))
              .addLast(new ServiceMessageDecoder())
              .addLast(new ServerHandler());
          }
        })
        .option(ChannelOption.SO_BACKLOG, 128)          // (5)
        .childOption(ChannelOption.SO_KEEPALIVE, true); // (6)

    // Bind and start to accept incoming connections.
    ChannelFuture f = b.bind(8888).sync();

    // Wait until the server socket is closed.
    // In this example, this does not happen, but you can do that to gracefully
    // shut down your server.
    f.channel().closeFuture().addListener(t -> {
      workerGroup.shutdownGracefully();
      bossGroup.shutdownGracefully();
    });
  }

}
