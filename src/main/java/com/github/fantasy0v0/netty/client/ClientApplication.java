package com.github.fantasy0v0.netty.client;

import io.netty.bootstrap.Bootstrap;
import io.netty.buffer.ByteBuf;
import io.netty.buffer.Unpooled;
import io.netty.channel.ChannelFuture;
import io.netty.channel.ChannelInitializer;
import io.netty.channel.ChannelOption;
import io.netty.channel.EventLoopGroup;
import io.netty.channel.nio.NioEventLoopGroup;
import io.netty.channel.socket.SocketChannel;
import io.netty.channel.socket.nio.NioSocketChannel;

import java.nio.charset.StandardCharsets;

public class ClientApplication {

  public static void test() throws InterruptedException {
    EventLoopGroup workerGroup = new NioEventLoopGroup();

    try {
      Bootstrap b = new Bootstrap(); // (1)
      b.group(workerGroup); // (2)
      b.channel(NioSocketChannel.class); // (3)
      b.option(ChannelOption.SO_KEEPALIVE, true); // (4)
      b.handler(new ChannelInitializer<SocketChannel>() {
        @Override
        public void initChannel(SocketChannel ch) throws Exception {
          ch.pipeline().addLast(new ClientHandler());
        }
      });

      // Start the client.
      ChannelFuture f = b.connect("127.0.0.1", 8888).sync(); // (5)

      // Wait until the connection is closed.
      f.channel().closeFuture().sync();
    } finally {
      workerGroup.shutdownGracefully();
    }
  }

  public static void main(String[] args) throws InterruptedException {
    test();
  }

}
